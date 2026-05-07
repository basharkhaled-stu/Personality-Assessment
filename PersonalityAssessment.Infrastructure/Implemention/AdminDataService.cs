using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Core.Admin;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.UnitOfWork;
using PersonalityAssessment.Infrastructure.Data;
using PersonalityAssessment.Infrastructure.User;

namespace PersonalityAssessment.Infrastructure.Implemention
{
    public class AdminDataService : IAdminDataService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminDataService(
            ApplicationDbContext db,
            UserManager<AppUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _db = db;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<AdminDashboardOverviewData> GetDashboardOverviewAsync(CancellationToken cancellationToken = default)
        {
            var totalUsers = await _db.Users.AsNoTracking().CountAsync(cancellationToken);

            var ua = _db.UsersAssessments.AsNoTracking();
            var totalAssessments = await ua.CountAsync(cancellationToken);
            var completed = await ua.CountAsync(x => x.CompletedAt != null, cancellationToken);
            var pending = await ua.CountAsync(x => x.CompletedAt == null, cancellationToken);

            var totalQuestions = await _db.Questions.AsNoTracking().CountAsync(cancellationToken);
            var totalPersonalityTypes = await _db.PersonalityTypes.AsNoTracking().CountAsync(cancellationToken);
            var totalAnswers = await _db.UserAnswers.AsNoTracking().CountAsync(cancellationToken);

            var ptSet = _db.Set<UsersAssessmentResultPersonalityType>().AsNoTracking();
            decimal? avgScore = await ptSet.AnyAsync(cancellationToken)
                ? await ptSet.AverageAsync(x => x.Score, cancellationToken)
                : null;

            var topPt = await ptSet
                .GroupBy(x => x.PersonalityTypeId)
                .Select(g => new { PersonalityTypeId = g.Key, Cnt = g.Count() })
                .OrderByDescending(x => x.Cnt)
                .FirstOrDefaultAsync(cancellationToken);

            string? topName = null;
            int? topCount = null;
            if (topPt != null)
            {
                topCount = topPt.Cnt;
                topName = await _db.PersonalityTypes.AsNoTracking()
                    .Where(p => p.Id == topPt.PersonalityTypeId)
                    .Select(p => p.Name)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            return new AdminDashboardOverviewData
            {
                TotalUsers = totalUsers,
                TotalAssessments = totalAssessments,
                TotalCompletedAssessments = completed,
                TotalPendingAssessments = pending,
                TotalQuestions = totalQuestions,
                TotalPersonalityTypes = totalPersonalityTypes,
                TotalAnswersSubmitted = totalAnswers,
                MostCommonPersonalityTypeName = topName,
                MostCommonPersonalityTypeCount = topCount,
                AverageAssessmentScore = avgScore,
            };
        }

        public async Task<AdminPagedUsersData> GetUsersPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _db.Users.AsNoTracking().OrderBy(u => u.UserName);
            var total = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new AdminUserSummaryData
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    EmailConfirmed = u.EmailConfirmed,
                    IsGoogleAccount = u.IsGoogleAccount,
                })
                .ToListAsync(cancellationToken);

            return new AdminPagedUsersData
            {
                Items = items,
                TotalCount = total,
                Page = page,
                PageSize = pageSize,
            };
        }

        public async Task<AdminUserDetailData?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var u = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            if (u == null)
                return null;

            var tracked = await _userManager.FindByIdAsync(userId);
            IReadOnlyList<string> roles = tracked != null
                ? (await _userManager.GetRolesAsync(tracked)).ToList()
                : Array.Empty<string>();

            return new AdminUserDetailData
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                EmailConfirmed = u.EmailConfirmed,
                IsGoogleAccount = u.IsGoogleAccount,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                TwoFactorEnabled = u.TwoFactorEnabled,
                LockoutEnabled = u.LockoutEnabled,
                LockoutEnd = u.LockoutEnd,
                Roles = roles,
                CreatedAtApprox = null,
            };
        }

        public async Task<bool> TryDeleteUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<List<AdminAssessmentListItemData>> GetAssessmentsAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Assessments
                .AsNoTracking()
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AdminAssessmentListItemData
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    AssessmentStatusId = a.AssessmentStatusId,
                    AssessmentStatusName = a.AssessmentStatus.Name,
                    AssessmentTypeId = a.AssessmentTypeId,
                    AssessmentTypeName = a.AssessmentType.Name,
                    QuestionCount = a.Questions.Count(),
                    CreatedAt = a.CreatedAt,
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<AdminAssessmentDetailData?> GetAssessmentByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var row = await _db.Assessments
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AdminAssessmentDetailData
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    AssessmentStatusId = a.AssessmentStatusId,
                    AssessmentStatusName = a.AssessmentStatus.Name,
                    AssessmentTypeId = a.AssessmentTypeId,
                    AssessmentTypeName = a.AssessmentType.Name,
                    QuestionCount = a.Questions.Count(),
                    CreatedAt = a.CreatedAt,
                })
                .FirstOrDefaultAsync(cancellationToken);

            return row;
        }

        public async Task<bool> TryDeleteAssessmentAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Assessments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            if (entity == null)
                return false;

            _db.Assessments.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<AdminAnalyticsData> GetAnalyticsAsync(int trendDays, CancellationToken cancellationToken = default)
        {
            trendDays = Math.Clamp(trendDays, 1, 90);
            var start = DateTime.UtcNow.Date.AddDays(-trendDays + 1);

            var dailyRaw = await _db.UsersAssessments
                .AsNoTracking()
                .Where(x => x.CreatedAt >= start)
                .GroupBy(x => x.CreatedAt.Date)
                .Select(g => new { Day = g.Key, Cnt = g.Count() })
                .OrderBy(x => x.Day)
                .ToListAsync(cancellationToken);

            var daily = dailyRaw
                .Select(x => new DailyAssessmentTrendPoint
                {
                    Date = DateOnly.FromDateTime(x.Day),
                    Count = x.Cnt,
                })
                .ToList();

            var activeUsers = await _db.UsersAssessments
                .AsNoTracking()
                .GroupBy(x => x.UserId)
                .Select(g => new { UserId = g.Key, Cnt = g.Count() })
                .OrderByDescending(x => x.Cnt)
                .Take(15)
                .ToListAsync(cancellationToken);

            var userIds = activeUsers.Select(x => x.UserId).ToList();
            var userMap = await _db.Users.AsNoTracking()
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => new { u.UserName, u.Email }, cancellationToken);

            var mostActive = activeUsers.Select(x => new ActiveUserRankData
            {
                UserId = x.UserId,
                UserName = userMap.TryGetValue(x.UserId, out var m) ? m.UserName : null,
                Email = userMap.TryGetValue(x.UserId, out var m2) ? m2.Email : null,
                AssessmentCount = x.Cnt,
            }).ToList();

            var qUsage = await _db.UserAnswers
                .AsNoTracking()
                .GroupBy(x => x.QuestionId)
                .Select(g => new { QuestionId = g.Key, Cnt = g.Count() })
                .OrderByDescending(x => x.Cnt)
                .Take(15)
                .ToListAsync(cancellationToken);

            var qIds = qUsage.Select(x => x.QuestionId).ToList();
            var qTexts = await _db.Questions.AsNoTracking()
                .Where(q => qIds.Contains(q.Id))
                .ToDictionaryAsync(q => q.Id, q => q.Text, cancellationToken);

            var mostQuestions = qUsage.Select(x => new QuestionUsageRankData
            {
                QuestionId = x.QuestionId,
                QuestionText = qTexts.TryGetValue(x.QuestionId, out var t) ? t : "",
                AnswerCount = x.Cnt,
            }).ToList();

            var oUsage = await _db.UserAnswers
                .AsNoTracking()
                .GroupBy(x => x.OptionId)
                .Select(g => new { OptionId = g.Key, Cnt = g.Count() })
                .OrderByDescending(x => x.Cnt)
                .Take(15)
                .ToListAsync(cancellationToken);

            var oIds = oUsage.Select(x => x.OptionId).ToList();
            var oTexts = await _db.Options.AsNoTracking()
                .Where(o => oIds.Contains(o.Id))
                .ToDictionaryAsync(o => o.Id, o => o.Text, cancellationToken);

            var mostOptions = oUsage.Select(x => new OptionUsageRankData
            {
                OptionId = x.OptionId,
                OptionText = oTexts.TryGetValue(x.OptionId, out var t) ? t : "",
                SelectionCount = x.Cnt,
            }).ToList();

            var totalUa = await _db.UsersAssessments.AsNoTracking().CountAsync(cancellationToken);
            var completedUa = await _db.UsersAssessments.AsNoTracking().CountAsync(x => x.CompletedAt != null, cancellationToken);
            var completionRate = totalUa > 0 ? (double)completedUa / totalUa * 100d : 0d;

            return new AdminAnalyticsData
            {
                DailyAssessmentsTrend = daily,
                MostActiveUsers = mostActive,
                MostUsedQuestions = mostQuestions,
                MostSelectedOptions = mostOptions,
                AverageCompletionRatePercent = Math.Round(completionRate, 2),
            };
        }

        public async Task<AdminDatabaseHealthData> GetDatabaseHealthAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var ok = await _db.Database.CanConnectAsync(cancellationToken);
                return new AdminDatabaseHealthData { CanConnect = ok };
            }
            catch (Exception ex)
            {
                return new AdminDatabaseHealthData { CanConnect = false, ErrorMessage = ex.Message };
            }
        }
    }
}

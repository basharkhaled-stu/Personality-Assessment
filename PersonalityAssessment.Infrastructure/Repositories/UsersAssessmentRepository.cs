using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Infrastructure.Data;

namespace PersonalityAssessment.Infrastructure.Repositories
{
    public class UsersAssessmentRepository : IUsersAssessmentRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersAssessmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UsersAssessment?> GetByIdWithResultsAsync(int id)
        {
            return await _context.UsersAssessments
                .Include(ua => ua.UsersAssessmentResults)
                    .ThenInclude(r => r.UsersAssessmentResultPersonalityType)
                .FirstOrDefaultAsync(ua => ua.Id == id);
        }

        // ← Include PersonalityType so Name/Label are not null
        public async Task<List<OptionPersonalityScore>> GetPersonalityScoresByOptionsAsync(List<int> optionIds)
        {
            return await _context.OptionPersonalityScores
                .Include(ops => ops.PersonalityType)
                .Where(ops => optionIds.Contains(ops.OptionId))
                .ToListAsync();
        }

        public async Task<List<PersonalityType>> GetAllPersonalityTypesAsync()
        {
            return await _context.PersonalityTypes.ToListAsync();
        }

        public async Task<List<Strength>> GetStrengthsByPersonalityTypeIdsAsync(List<int> personalityTypeIds)
        {
            return await _context.Strengths
                .Where(s => personalityTypeIds.Contains(s.PersonalityTypeId))
                .ToListAsync();
        }

        public async Task<List<Weakness>> GetWeaknessesByPersonalityTypeIdsAsync(List<int> personalityTypeIds)
        {
            return await _context.Weaknesses
                .Where(w => personalityTypeIds.Contains(w.PersonalityTypeId))
                .ToListAsync();
        }

        public async Task<UserAssessmentStatus?> GetCompletedStatusAsync()
        {
            return await _context.UserAssessmentStatuses
                .FirstOrDefaultAsync(uas => uas.Name.ToLower() == "completed");
        }
    }
}

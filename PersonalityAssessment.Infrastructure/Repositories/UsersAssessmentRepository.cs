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
            return await _context.UsersAssessments.FindAsync(id);
        }

        public async Task<List<OptionPersonalityScore>> GetPersonalityScoresByOptionsAsync(List<int> optionIds)
        {
            return await _context.OptionPersonalityScores
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
            // Temporarily return empty list to isolate compilation issue
            await Task.CompletedTask;
            return new List<Weakness>();
        }

        public async Task<UserAssessmentStatus?> GetCompletedStatusAsync()
        {
            return await _context.UserAssessmentStatuses
                .FirstOrDefaultAsync(uas => uas.Name.ToLower() == "completed");
        }
    }
}

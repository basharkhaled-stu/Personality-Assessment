using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Core.Interface
{
    public interface IUsersAssessmentRepository
    {
        Task<UsersAssessment?> GetByIdWithResultsAsync(int id);
        Task<List<OptionPersonalityScore>> GetPersonalityScoresByOptionsAsync(List<int> optionIds);
        Task<List<PersonalityType>> GetAllPersonalityTypesAsync();
        Task<List<Strength>> GetStrengthsByPersonalityTypeIdsAsync(List<int> personalityTypeIds);
        Task<List<Weakness>> GetWeaknessesByPersonalityTypeIdsAsync(List<int> personalityTypeIds);
        Task<UserAssessmentStatus?> GetCompletedStatusAsync();
    }
}

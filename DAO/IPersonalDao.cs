using PersonalAPI.Models;

namespace PersonalAPI.DAO
{
    public interface IPersonalDao
    {
        Task<List<PersonalInformationData>> GetAllPersonalInfo(string baseUri);
        Task<int> InsertPersonalDetails(InsertPersonalDetails personalInfo, string imageName);
    }
}

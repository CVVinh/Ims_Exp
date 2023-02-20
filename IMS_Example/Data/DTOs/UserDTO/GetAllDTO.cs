
using IMS_Example.Data.Models;

namespace IMS_Example.Data.DTOs.UserDTO
{
    public class GetAllDTO : Users
    {
        public GetAllDTO(Users user)
        {
            id = user.id;
            userCode = user.userCode;
            userPassword = user.userPassword;
            userCreated = user.userCreated;
            dateCreated = user.dateCreated;
            userModified = user.userModified;
            dateModified = user.dateModified;
            firstName = user.firstName;
            lastName = user.lastName;
            phoneNumber = user.phoneNumber;
            dOB = user.dOB;
            gender = user.gender;
            address = user.address;
            university = user.university;
            yearGraduated = user.yearGraduated;
            email = user.email;
            skype = user.skype;
            workStatus = user.workStatus;
            dateStartWork = user.dateStartWork;
            dateLeave = user.dateLeave;
            maritalStatus = user.maritalStatus;
            reasonResignation = user.reasonResignation;
            identitycard = user.identitycard;
            IdGroup = user.IdGroup;
            isDeleted = user.isDeleted;
        }
    }
}

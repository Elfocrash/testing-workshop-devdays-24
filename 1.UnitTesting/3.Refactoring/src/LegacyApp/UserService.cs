using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly TimeProvider _timeProvider;
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditServiceClientFactory _userCreditServiceClientFactory;
        private readonly IUserDataAccessAdapter _accessAdapter;
        
        public UserService(TimeProvider timeProvider, 
            IClientRepository clientRepository,
            IUserCreditServiceClientFactory userCreditServiceClientFactory,
            IUserDataAccessAdapter accessAdapter)
        {
            _timeProvider = timeProvider;
            _clientRepository = clientRepository;
            _userCreditServiceClientFactory = userCreditServiceClientFactory;
            _accessAdapter = accessAdapter;
        }

        public UserService() : this(TimeProvider.System, 
            new ClientRepository(),
            new UserCreditServiceClientFactory(),
            new UserDataAccessAdapter())
        {
            
        }

        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = _timeProvider.GetLocalNow();
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }
            
            var client = _clientRepository.GetById(clientId);

            var user = new User
           {
               Client = client,
               DateOfBirth = dateOfBirth,
               EmailAddress = email,
               Firstname = firname,
               Surname = surname
           };

            if (client.Name == "VeryImportantClient")
            {
                // Skip credit check
                user.HasCreditLimit = false;
            }
            else if (client.Name == "ImportantClient")
            {
                // Do credit check and double credit limit
                user.HasCreditLimit = true;
                using (var userCreditService = _userCreditServiceClientFactory.CreateClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                    creditLimit = creditLimit*2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Do credit check
                user.HasCreditLimit = true;
                using (var userCreditService = _userCreditServiceClientFactory.CreateClient())
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            _accessAdapter.AddUser(user);
            return true;
        }
    }
}

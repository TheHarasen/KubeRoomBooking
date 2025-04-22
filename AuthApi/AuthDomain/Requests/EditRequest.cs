using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Requests;

public record EditRequest(Guid Id, string Email, string FirstName, string LastName, string Password);
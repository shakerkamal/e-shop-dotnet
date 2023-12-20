using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.DataTransferObjects.AuthenticationDtos;

public record AuthenticateUserDto(string Email, string Password);

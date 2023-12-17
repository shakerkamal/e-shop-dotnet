using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.DataTransferObjects.UserDtos;

public record CreateUserDto(string Name, string Email, string Password, bool IsAdmin);

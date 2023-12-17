using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.DataTransferObjects.UserDtos;

public record UserIndexDto(string UserId, string? Name, string? Email);

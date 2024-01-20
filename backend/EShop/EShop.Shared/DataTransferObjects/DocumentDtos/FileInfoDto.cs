using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.DataTransferObjects.DocumentDtos
{
    public record FileInfoDto(string Uri, string Name, string ContentType, Stream Content);
}

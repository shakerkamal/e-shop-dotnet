namespace EShop.Shared.DataTransferObjects.DocumentDtos
{
    public record FileInfoDto(string Uri, string Name, string ContentType, Stream Content);
}

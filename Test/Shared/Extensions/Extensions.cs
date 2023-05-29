using Test.Shared.DTOS;
using Test.Shared.Models;

namespace Test.Shared.Extensions
{
    public static class Extensions
    {
        public static UserDTO UserAsDTO(this User user)
        {
            return new UserDTO
            {
                ID = user.ID,
                Email = user.Email,
                Username = user.Username,
                Type = user.Type
            };
        }

        public static ProductDTO ProductAsDTO(this Product product)
        {
            return new ProductDTO
            {
                ID = product.ID,
                Description = product.Description,
                ImageData = product.ImageData,
                MimeType = product.MimeType,
                Model = product.Model,
                Name = product.Name,
                Price = product.Price,
                UserID = product.UserID
            };
        }
    }
}

using Application.Common.Interfaces.ContentGenerator.Common;
using OpenAI.ObjectModels;

namespace Infrastructure.ContentGenerator.OpenAI
{
    public class TextDavinciV3Model : IContentModel
    {
        public string ModelId => Models.TextDavinciV3;
    }
}

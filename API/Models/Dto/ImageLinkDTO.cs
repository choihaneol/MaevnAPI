namespace API.Models.Dto
{
    #nullable enable
    public class ImageLinkDTO
    {
        public List<ImageSingleDTO> list { get; set; }

        public List<ImageSingleDTO> colordot { get; set; }

        public List<ImageMultipleDTO> detail { get; set; }

        public List<ImageSingleDTO> related { get; set; }
    }
}

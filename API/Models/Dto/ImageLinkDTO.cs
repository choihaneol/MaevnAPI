namespace API.Models.Dto
{
#nullable enable
    public class ImageLinkDTO
    {
        public int seq { get; set; }
        public string colorCode { get; set; }
        public string colorName { get; set; }
        public string colordotURL { get; set; }
        public List<ImageMultipleDTO> detail { get; set; }
        public List<ImageSingleDTO> related { get; set; }
    }
}

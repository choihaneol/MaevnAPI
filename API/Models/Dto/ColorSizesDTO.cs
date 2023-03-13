namespace API.Models.Dto
{
    public class ColorSizesDTO
    {
        public int seq { get; set; }
        public string colorCode { get; set; }
        public string colorName { get; set; }

        public List<SizeDTO> sizes { get; set; }

    }
}

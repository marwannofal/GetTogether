namespace API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSiza = 550;
        public int pageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize 
        { 
            get => _pageSize;
            set => _pageSize = (value > MaxPageSiza) ? MaxPageSiza : value;
        }

        public string CurrentUsername { get; set; }
        public string Gender { get; set; }
    }
}
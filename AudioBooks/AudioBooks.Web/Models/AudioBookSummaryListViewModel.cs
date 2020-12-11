using AudioBooks.Model;
using System.Collections.Generic;

namespace AudioBooks.Web.Models
{
    public class AudioBookSummaryListViewModel
    {
        public bool CanAddAudioBooks { get; set; }
        public IEnumerable<AudioBookItemSummaryModel> AudioBooks { get; private set; } = new List<AudioBookItemSummaryModel>();

        public AudioBookSummaryListViewModel(IEnumerable<AudioBookItemSummaryModel> list)
        {
            this.AudioBooks = list;
            this.CanAddAudioBooks = false;
        }
    }
}

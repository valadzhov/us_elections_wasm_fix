using System.Reactive.Subjects;

namespace USElections.State
{
    public interface IStateService
    {
        public BehaviorSubject<double> CurrentlyChosenYear { get; set; }
    }
}

using System.Reactive.Subjects;

namespace USElections.State
{
    public class StateService: IStateService
    {
        public BehaviorSubject<double> CurrentlyChosenYear { get; set; } = new(2024);
    }
}

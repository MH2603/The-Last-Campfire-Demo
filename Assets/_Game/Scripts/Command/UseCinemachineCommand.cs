using UnityEngine;

namespace MH.Command
{
    public class UseCinemachineCommand : BaseCommand
    {
        [SerializeField] private string cinemachineName;
        
        private ICinemachineService _cinemachineService;

        public override void Execute()
        {
            if (_cinemachineService == null)
            {
                _cinemachineService = ServiceLocator.Instance.GetService<ICinemachineService>();
            }
            
            _cinemachineService.UseCinemachine(cinemachineName);
        }
    }
}
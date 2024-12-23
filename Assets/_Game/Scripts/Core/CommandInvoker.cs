using System.Collections.Generic;
using UnityEngine;

namespace MH
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
        
    }

    public abstract class BaseCommand : EntityComponent, ICommand
    {
        public virtual void Execute()
        {
            
        }

        public virtual void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class CommandInvoker : BaseEntity
    {
        [SerializeField] private BaseCommand[] _commands;
        [SerializeField] private bool _autoFindCommands = true;

        protected override void Start()
        {
            base.Start();
            if (_autoFindCommands)
            {
                _commands = GetComponentsInChildren<BaseCommand>();
            }
        }
        
        public void ExecuteAllCommands()
        {
            for (int i=0; i < _commands.Length; i++)
            {
                _commands[i].Execute();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FSM
{
    public class PlayerMachine : StateMachine
    {
        public PlayerMachine(GameObject owner) : base(owner)
        {
            Initialize(StateDataSheet.GetPlayerData(this));
        }
    }
}

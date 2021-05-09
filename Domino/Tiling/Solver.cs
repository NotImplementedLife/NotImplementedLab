using NotImplementedLab.Controls.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino.Tiling
{
    public abstract class Solver
    {
        protected Model Model;

        public Solver(Model model, int maxDim)
        {
            Model = model;
            MaxDim = maxDim;            
        }

        public int MaxDim { get; }

        public abstract void Init();

        public bool IsReady { get; protected set; } = false;

        public abstract void NextStep();

        public List<Domi> PartialSol { get; } = new List<Domi>();

        public abstract string Name { get; }

        public ModalDialogBase PropertiesModal = null;        

    }
}

using UnityEngine;
using System.Collections;

namespace GameCore
{
    public class EntityState : StateBase
    {
        private GameObject _baseEntity;
        private GameObject _gameObject;
        public EntityState(int stateid, EntityState parent)
    :   base(stateid, parent)
        {
            _baseEntity = null;
            _gameObject = null;
        }
        //-------------------------------------------------------------------------
        public EntityState(int stateid, EntityState parent, GameObject gameObject)
            : base(stateid, parent)
        {
            _gameObject = gameObject;
        }
        //-------------------------------------------------------------------------
        public void SetNextSubState(int stateid)
        {
            base.NextSubStateID = stateid;
        }
        //-------------------------------------------------------------------------
        public bool GotoSubState(int stateid)
        {
            return base.SetSubState(stateid, null);
        }
        //-------------------------------------------------------------------------
        public bool GotoSubState(int stateid, object[] command)
        {
            return base.SetSubState(stateid, command);
        }
        //-------------------------------------------------------------------------
        public bool GotoBrotherState(int stateid)
        {
            return GetParent().GotoSubState(stateid, null);
        }
        //-------------------------------------------------------------------------
        public bool GotoBrotherState(int stateid, object[] command)
        {
            return GetParent().GotoSubState(stateid, command);
        }
        //-------------------------------------------------------------------------
        protected GameObject GetGameObject()
        {
            if (Object.ReferenceEquals(GetParent(), null))
                return _gameObject;

            return GetParent().GetGameObject();
        }
        //-------------------------------------------------------------------------
        protected EntityState GetParent()
        {
            return (EntityState)GetParentState();
        }
        //-------------------------------------------------------------------------
        public int GetSubStateID()
        {
            if (Object.ReferenceEquals(GetSubState(), null))
                return -1;

            return GetSubState().StateID;
        }
		public int GetOldStateID(){
			return OldSubStateID;
		}
    }
}
//-------------------------------------------------------------------------


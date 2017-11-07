using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


namespace GameCore
{
    public class IResult : BaseObject
    {
        public enum RESULT
        {
            CONTINUOUS,
            FINISH,
        }
        public RESULT Result { get; set; }
    }
    public class MSGPipe : BaseObject
    {
        public void SendCommand(string methodName)
        {
            IResult result = new IResult();
            result.Result = IResult.RESULT.FINISH;
            object[] parameter = new object[] { result };

            ProccessCommand(methodName, parameter);
            if (result.Result == IResult.RESULT.CONTINUOUS)
            {
                OnCommand(methodName, parameter);
            }
        }

        public void SendCommand(string methodName, object value)
        {
            IResult result = new IResult();
            result.Result = IResult.RESULT.FINISH;
            object[] parameter = new object[] { result, value };

            ProccessCommand(methodName, parameter);
            if (result.Result == IResult.RESULT.CONTINUOUS)
            {
                OnCommand(methodName, parameter);
            }
        }

        public void SendCommand(string methodName, object value1, object value2)
        {
            IResult result = new IResult();
            result.Result = IResult.RESULT.FINISH;
            object[] parameter = new object[] { result, value1, value2 };
            ProccessCommand(methodName, parameter);
            if (result.Result == IResult.RESULT.CONTINUOUS)
            {
                OnCommand(methodName, parameter);
            }
        }

        public void SendCommand(string methodName, object[] value)
        {
            IResult result = (IResult)value[0];
            object[] parameter = value;

            ProccessCommand(methodName, parameter);
            if (result.Result == IResult.RESULT.CONTINUOUS)
            {
                OnCommand(methodName, parameter);
            }
        }

        protected virtual void OnCommand(string methodName, object[] parameter)
        {
            IResult result = (IResult)parameter[0];
            result.Result = IResult.RESULT.FINISH;
        }

        protected void ProccessCommand(string methodName, object[] parameter)
        {
            IResult result = (IResult)parameter[0];
            MethodInfo method = GetType().GetMethod(methodName);
            if (method == null)
            {
                result.Result = IResult.RESULT.CONTINUOUS;
                return;
            }

            result.Result = IResult.RESULT.FINISH;
            method.Invoke(this, parameter);
        }
    }
    //-------------------------------------------------------------------------
    public class StateBase : MSGPipe
    {
        private Dictionary<int, StateBase> 	EntityStateDict;
        public  StateBase 					_currentSubState;

        public  int 						StateID { get; set; }
        private StateBase 					_parentState;

        public StateBase ParentState
        {
            get
            {
                return _parentState;
            }
        }

        public int CurrentSubStateID()
        {
            //if (_currentSubState == null)
            //{
            //    return -1;
            //}

            //return _currentSubState.StateID;
            try
            {
                return _currentSubState.StateID;
            }
            catch (System.Exception)
            {

                return -1;
            }
        }

        public int NextSubStateID { get; set; }

        public int OldSubStateID { get; set; }

        public StateBase(int stateid, StateBase parent)
        {
            StateID = stateid;
            _parentState = parent;
            EntityStateDict = new Dictionary<int, StateBase>();
        }
        //-------------------------------------------------------------------------
        public void Init()
        {
            OnStateInit();
        }
        //-------------------------------------------------------------------------
        public void Update()
        {
            OnUpdate();

            if (Object.ReferenceEquals(_currentSubState, null))
            {
                SetSubState(NextSubStateID, null);
            }
            else if (NextSubStateID != _currentSubState.StateID)
            {
                SetSubState(NextSubStateID, null);
            }

            if (!Object.ReferenceEquals(_currentSubState, null))
            {
                _currentSubState.Update();
            }
        }
        //-------------------------------------------------------------------------
        protected virtual void OnStateInit()
        {
            _currentSubState = null;
            foreach (StateBase state in EntityStateDict.Values)
            {
                state.OnStateInit();
            }
        }
        //-------------------------------------------------------------------------
        protected virtual void OnStateDestroy()
        {
            _currentSubState = null;
        }
        //-------------------------------------------------------------------------
        protected virtual void OnStateBegin(object[] command)
        {
            //
        }
        //-------------------------------------------------------------------------
        protected virtual void OnStateEnd()
        {
            //
        }
        public virtual void OnStateLateEnd()
        {

        }
        //-------------------------------------------------------------------------
        protected virtual void OnUpdate()
        {
            //
        }
        //-------------------------------------------------------------------------
        protected void AddSubState(StateBase substate)
        {
            EntityStateDict.Add(substate.StateID, substate);
        }
        //-------------------------------------------------------------------------
        protected void RemoveSubState(StateBase substate)
        {
            EntityStateDict.Remove(substate.StateID);
        }
        //-------------------------------------------------------------------------
        protected void BeginState(object[] command)
        {
            OnStateBegin(command);

            //if (!Object.ReferenceEquals(_currentSubState, null))
            //{
            //    _currentSubState.BeginState(command);
            //}
        }
        //-------------------------------------------------------------------------
        protected void EndState()
        {
            if (!Object.ReferenceEquals(_currentSubState, null))
            {
                _currentSubState.EndState();
            }

            OnStateEnd();
        }
        //-------------------------------------------------------------------------
        protected bool SetSubState(int stateid, object[] command)
        {
            StateBase state        = null;
            StateBase _OldSubState = null;

            if (EntityStateDict.TryGetValue(stateid, out state))
            {
                if (!Object.ReferenceEquals(_currentSubState, null))
                {
                    OldSubStateID = _currentSubState.StateID;
                    _OldSubState  = _currentSubState;
                    _currentSubState.EndState();
                }

                NextSubStateID      = state.StateID;
                _currentSubState    = state;
                _currentSubState.BeginState(command);

				if (!Object.ReferenceEquals(_OldSubState, null))
					_OldSubState.OnStateLateEnd();

                OnSubStateChanged(command);
                return true;
            }
            return false;
        }
        //-------------------------------------------------------------------------
        protected StateBase GetParentState()
        {
            return _parentState;
        }
        //-------------------------------------------------------------------------
        public StateBase GetSubState()
        {
            return _currentSubState;
        }
        //-------------------------------------------------------------------------
        protected override void OnCommand(string methodName, object[] parameter)
        {
            //ProccessCommand(methodName, parameter);
            IResult result = (IResult)parameter[0];

            if (result.Result == IResult.RESULT.FINISH)
            {
                return;
            }

            if (Object.ReferenceEquals(_currentSubState, null))
            {
                return;
            }

            _currentSubState.SendCommand(methodName, parameter);
        }
        //-------------------------------------------------------------------------
        protected virtual void OnSubStateChanged(object[] command)
        {
            //
        }
        public virtual void OnAnimationEnd(string strName)
        {
            if (Object.ReferenceEquals(_currentSubState, null))
            {
                return;
            }
            _currentSubState.OnAnimationEnd(strName);
        }

        /// <summary>
        /// 状态切换完成
        /// </summary>
        public virtual void OnFinishedMsg(string id)
        {

        }
    }
}


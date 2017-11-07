using System;
using com.guojin.core.io;
using com.guojin.core.io.message;
using System.Collections.Generic;

/**
 * 斗牛牌局结束
 * 
 * <b>生成器生成代码，请勿修改，扩展请继承</b>
 * @author isnowfox消息生成器
 */
namespace com.guojin.dn.net.message {
    public class DouniuGameChapterEnd : Message {
        public static int TYPE = 5;
        public static int ID = 22;

        private int zhuangIndex;  //牛牛位置，赢家的位置
        private List<DouniuCompareResult> compareResultList;//牌的信息
        //private List<DouniuCompareResult> compareResultList1;   //牌的信息

        //public List<DouniuCompareResult> CompareResultList1 {
        //    get {
        //        return compareResultList1;
        //    }

        //    set {
        //        compareResultList1 = value;
        //    }
        //}

        public int getZhuangIndex() {
            return zhuangIndex;
        }

        public void setZhuangIndex(int zhuangIndex) {
            this.zhuangIndex = zhuangIndex;
        }

        public List<DouniuCompareResult> getCompareResultList() {
            return compareResultList;
        }

        public void setCompareResultList(List<DouniuCompareResult> compareResultList) {
            this.compareResultList = compareResultList;
        }

        public DouniuGameChapterEnd() {

        }

        public DouniuGameChapterEnd(int zhuangIndex, List<DouniuCompareResult> compareResultList) {
            this.zhuangIndex = zhuangIndex;
            this.compareResultList = compareResultList;
        }
        
    public void decode(Input _in)   {

        zhuangIndex = _in.readInt();
        int count = _in.readInt();
		if(count == -1){
			compareResultList = null ; 
		}else{
			compareResultList = new List<DouniuCompareResult>();
			for(int i = 0; i<count ; i ++){
                    DouniuCompareResult result = new DouniuCompareResult();
    result.decode(_in);
				compareResultList.Add(result);
			}
		}
	}

    public void encode(Output _out)  {
            _out.writeInt(zhuangIndex);
		if(compareResultList == null){
                _out.writeInt(-1);
		}else{
			List<DouniuCompareResult> compareResultList = getCompareResultList();
                _out.writeInt(compareResultList.Count);
			 foreach(DouniuCompareResult res in compareResultList){
				 res.encode(_out);
			 }
		}
	}




        //public void decode(Input _in) {

        //    zhuangIndex = _in.readInt();
        //    int count = _in.readInt();
        //    if (count == -1) {
        //        compareResultList = null;
        //    } else {
        //        compareResultList = new List<DouniuCompareResult>();
        //        //for (int i = 0; i < count; i++) {
        //        //    DouniuCompareResult result = new DouniuCompareResult();
        //        //    result.decode(_in);
        //        //}
        //    }
        //}

        //public void encode(Output _out) {
        //    _out.writeInt(zhuangIndex);
        //    if (compareResultList == null) {
        //        _out.writeInt(-1);
        //    } else {
        //        _out.writeInt(compareResultList.Count);
        //        for (int i = 0; i < compareResultList.Count; i++) {
        //            DouniuCompareResult result = compareResultList[i];
        //            result.encode(_out);
        //        }
        //    }
        //}

        public string toString() {
            return "DouniuGameChapterEnd [niuniuIndex=" + ", ]";
        }
        public int getMessageType() {
            return TYPE;
        }

        public int getMessageId() {
            return ID;
        }
    }
}

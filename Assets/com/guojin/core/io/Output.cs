using com.guojin.core.io.message;
using System.Collections.Generic;

namespace com.guojin.core.io
{
/*
 * 消息输出接口
 */
public interface Output
{
        void writeBoolean(bool v);

        void writeInt(int v);

        void writeLong(long v);

        void writeFloat(float v);

        void writeDouble(double v);

        void writeString(string s);

        void writeBytes(Byte bs);

        void writeByteBuf(Byte bs);

        void writeBooleanArray(bool[] v);

        void writeIntArray(int[] v);

        void writeLongArray(long[] v);

        void writeFloatArray(float[] v);

        void writeDoubleArray(double[] v);

        void writeStringArray(string[] v);
        void writeList<T>(Output _in,List<T> t) where T : Message, new();
        void writeT<T>(Output _in,T t) where T : Message, new();
    }

}
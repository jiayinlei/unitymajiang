using com.guojin.core.io.message;
using System.Collections.Generic;

namespace com.guojin.core.io
{
    public interface Input
    {
        bool readBoolean() ;

        int readInt();

        long readLong();

        float readFloat();

        double readDouble();

        string readString();

        Byte readBytes();

        Byte readByteBuf();

        bool[] readBooleanArray();

        int[] readIntArray();

        long[] readLongArray();

        float[] readFloatArray();

        double[] readDoubleArray();

        string[] readStringArray();
        List<T> readList<T>(Input _in) where T : Message, new();
        T readT<T>(Input _in) where T : Message, new();
        Byte getDataInput();
    }
}
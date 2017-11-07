using com.guojin.core.io.message;
using System.Collections.Generic;

namespace com.guojin.core.io
{
    public class AbstractInput : Input
    {
        protected Byte mIn;
        protected string mCharset;

        public AbstractInput(Byte in0, string charset)
        {
            mIn = in0;
            mCharset = charset;
        }

        public Byte getDataInput()
        {
            return mIn;
        }
        public virtual List<T> readList<T>(Input _in) where T : Message, new()
        {
            throw new AbstractMethodException();
        }
        public virtual T readT<T>(Input _in) where T : Message, new()
        {
            throw new AbstractMethodException();
        }
        public virtual bool readBoolean()
        {
            throw new AbstractMethodException();
        }

        public virtual int readInt()
        {
            throw new AbstractMethodException();
        }

        public virtual long readLong()
        {
            throw new AbstractMethodException();
        }

        public virtual float readFloat()
        {
            throw new AbstractMethodException();
        }

        public virtual double readDouble()
        {
            throw new AbstractMethodException();
        }

        public virtual string readString()
        {
            throw new AbstractMethodException();
        }

        public virtual Byte readBytes()
        {
            throw new AbstractMethodException();
        }

        public virtual Byte readByteBuf()
        {
            throw new AbstractMethodException();
        }

        public virtual bool[] readBooleanArray()
        {
            int len = readInt();
            if (len == -1)
            {
                return null;
            }
            bool[] a = new bool[len];
            for (int i = 0; i < len; i++)
            {
                a[i] = readBoolean();
            }
            return a;
        }

        public int[] readIntArray()
        {
            int len = readInt();
            if (len == -1)
            {
                return null;
            }
            int[] a = new int[len];
            for (int i = 0; i < len; i++)
            {
                a[i] = readInt();
            }
            return a;
        }

        public virtual long[] readLongArray()
        {
            int len = readInt();
            if (len == -1)
            {
                return null;
            }
            long[] a = new long[len];
            for (int i = 0; i < len; i++)
            {
                a[i] = readLong();
            }
            return a;
        }

        public virtual float[] readFloatArray()
        {
            int len = readInt();
            if (len == -1)
            {
                return null;
            }
            float[] a = new float[len];
            for (int i = 0; i < len; i++)
            {
                a[i] = readFloat();
            }
            return a;
        }

        public virtual double[] readDoubleArray()
        {
            int len = readInt();
            if (len == -1)
            {
                return null;
            }
            double[] a = new double[len];
            for (int i = 0; i < len; i++)
            {
                a[i] = readDouble();
            }
            return a;
        }

        public virtual string[] readStringArray()
        {
            int len = readInt();
            if (len == -1)
            {
                return null;
            }
            string[] a = new string[len];
            for (int i = 0; i < len; i++)
            {
                a[i] = readString();
            }
            return a;
        }
    }
}
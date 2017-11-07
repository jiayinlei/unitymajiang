using com.guojin.core.io.message;
using System.Collections.Generic;

namespace com.guojin.core.io
{
    /*
     * 消息输出抽象类
     */
    public abstract class AbstractOutput : Output
    {
        protected Byte mOut;
        protected string mCharset;

        public AbstractOutput(Byte _out, string charset)
        {
            mOut = _out;
            mCharset = charset;
        }

        public virtual void writeBoolean(bool v)
        {
            throw new AbstractMethodException();
        }

        public virtual void writeInt(int v)
        {
            throw new AbstractMethodException();
        }

        public virtual void writeLong(long v)
        {
            throw new AbstractMethodException();
        }

        public virtual void writeFloat(float v)
        {
            throw new AbstractMethodException();
        }

        public virtual void writeDouble(double v)
        {
            throw new AbstractMethodException();
        }

        public virtual void writeString(string s)
        {
            throw new AbstractMethodException();
        }

        public virtual void writeBytes(Byte bs)
        {
            throw new AbstractMethodException();
        }

        public virtual void writeList<T>(Output _in,List<T> t) where T : Message, new()
        {
            throw new AbstractMethodException();
        }
        public virtual void writeT<T>(Output _in,T t) where T : Message, new()
        {
            throw new AbstractMethodException();
        }
        public virtual void writeByteBuf(Byte bs)
        {
            if (bs == null)
            {
                writeInt(-1);
            }
            else
            {
                mOut.writeArrayBuffer(bs.buffer, (uint) 0, (uint) bs.length);
            }
        }

        public virtual void writeBooleanArray(bool[] v)
        {
            if (v == null)
            {
                writeInt(-1);
            }
            else
            {
                writeInt(v.Length);
                int len = v.Length;
                for (int i = 0; i < len; i++)
                {
                    writeBoolean(v[i]);
                }
            }
        }

        public virtual void writeIntArray(int[] v)
        {
            if (v == null)
            {
                writeInt(-1);
            }
            else
            {
                writeInt(v.Length);
                int len = v.Length;
                for (int i = 0; i < len; i++)
                {
                    writeInt(v[i]);
                }
            }
        }

        public virtual void writeLongArray(long[] v)
        {
            if (v == null)
            {
                writeInt(-1);
            }
            else
            {
                writeInt(v.Length);
                int len = v.Length;
                for (int i = 0; i < len; i++)
                {
                    writeLong(v[i]);
                }
            }
        }

        public virtual void writeFloatArray(float[] v)
        {
            if (v == null)
            {
                writeInt(-1);
            }
            else
            {
                writeInt(v.Length);
                int len = v.Length;
                for (int i = 0; i < len; i++)
                {
                    writeFloat(v[i]);
                }
            }
        }

        public virtual void writeDoubleArray(double[] v)
        {
            if (v == null)
            {
                writeInt(-1);
            }
            else
            {
                writeInt(v.Length);
                int len = v.Length;
                for (int i = 0; i < len; i++)
                {
                    writeDouble(v[i]);
                }
            }
        }

        public virtual void writeStringArray(string[] v)
        {
            if (v == null)
            {
                writeInt(-1);
            }
            else
            {
                writeInt(v.Length);
                int len = v.Length;
                for (int i = 0; i < len; i++)
                {
                    writeString(v[i]);
                }
            }
        }
    }
}
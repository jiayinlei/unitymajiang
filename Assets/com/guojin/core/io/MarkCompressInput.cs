using com.guojin.core.utils;
using System.Collections.Generic;

namespace com.guojin.core.io
{
    /**
     * 创建这个对象之后修改 endian会出现bug
     */
    public class MarkCompressInput : AbstractInput
    {
        public static MarkCompressInput create(Byte in0)
        {
            return new MarkCompressInput(in0, MarkCompressProtocol.DEFAULT_CHARSET);
        }

        public static MarkCompressInput createByCharset(Byte in0, string charset)
        {
            return new MarkCompressInput(in0, charset);
        }

        /**读取double的缓冲*/
        private Byte mDoubleBuffer = new Byte();

        public MarkCompressInput(Byte in0, string charset) : base(in0, charset)
        {
            mDoubleBuffer.endian = in0.endian;
        }
        public override List<T> readList<T>(Input _in) 
        {
            int Len = readInt();
            List<T> data = new List<T>();
            if (Len == -1)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < Len; i++)
                {
                    T t = new T();
                    t.decode(_in);
                    data.Add(t);
                }
            }   
            return data;
        }
        public override T readT<T>(Input _in)
        {
            bool bol = _in.readBoolean();
            T data = new T();
            if (bol)
            {
                data.decode(_in);
            }
            else
            {
                data = default(T);
            }
            return data;
        }
        public override bool readBoolean()
        {
            int b = (int) mIn.getUint8();
            if (b == MarkCompressProtocol.TYPE_TRUE)
            {
                return true;
            }
            else if (b == MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE)
            {
                return false;
            }
            else
            {
                throw ProtocolException.newTypeException(b);
            }
        }

        public override int readInt()
        {
            int b = (int) mIn.getUint8();

            switch (b)
            {
                case MarkCompressProtocol.TYPE_TRUE:
                    return 1;
                case MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE:
                    return 0;
                case MarkCompressProtocol.TYPE_INT_1BYTE:
                    return mIn.getUint8();
                case MarkCompressProtocol.TYPE_INT_2BYTE:
                    return (int) mIn.getUint16();
                case MarkCompressProtocol.TYPE_INT_3BYTE:
                    return Utils.getThree(mIn);
                case MarkCompressProtocol.TYPE_INT_4BYTE:
                    return mIn.getInt32();
                default:
                    if (b > MarkCompressProtocol.TYPE_MIN && b < MarkCompressProtocol.TYPE_MAX)
                    {
                        return b - MarkCompressProtocol.TYPE_MINUS;
                    }
                    else
                    {
                        throw  ProtocolException.newTypeException(b);
                    }
            }
        }

        public override long readLong()
        {
            int b = (int) mIn.getUint8();
            switch (b)
            {
                case MarkCompressProtocol.TYPE_TRUE:
                    return 1;
                case MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE:
                    return 0;
                case MarkCompressProtocol.TYPE_INT_1BYTE:
                    return mIn.getUint8();
                case MarkCompressProtocol.TYPE_INT_2BYTE:
                    return mIn.getUint16();
                case MarkCompressProtocol.TYPE_INT_3BYTE:
                    return Utils.getThree(mIn);
                case MarkCompressProtocol.TYPE_INT_4BYTE:
                    return mIn.getUint32();
                case MarkCompressProtocol.TYPE_INT_5BYTE:
                    return (long) getInt64Five();
                case MarkCompressProtocol.TYPE_INT_6BYTE:
                    return (long) getInt64Six();
                case MarkCompressProtocol.TYPE_INT_7BYTE:
                    return (long) getInt64Seven();
                case MarkCompressProtocol.TYPE_INT_8BYTE:
                    return (long) getInt64Long();
                default:
                    if (b > MarkCompressProtocol.TYPE_MIN && b < MarkCompressProtocol.TYPE_MAX)
                    {
                        return b - MarkCompressProtocol.TYPE_MINUS;
                    }
                    else
                    {
                        throw  ProtocolException.newTypeException(b);
                    }
            }
        }

        public override float readFloat()
        {
            int b = (int) mIn.getUint8();
            switch (b)
            {
                case MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE:
                    return 0;
                case MarkCompressProtocol.TYPE_INT_1BYTE:
                    return readFloatByNums(1);
                case MarkCompressProtocol.TYPE_INT_2BYTE:
                    return readFloatByNums(2);
                case MarkCompressProtocol.TYPE_INT_3BYTE:
                    return readFloatByNums(3);
                case MarkCompressProtocol.TYPE_INT_4BYTE:
                    return mIn.getFloat32();
                default:
                    throw ProtocolException.newTypeException(b);
            }
        }

        public override double readDouble()
        {
            int b = (int) mIn.getUint8();
            switch (b)
            {
                case MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE:
                    return 0;
                case MarkCompressProtocol.TYPE_INT_1BYTE:
                    return readDoubleByNums(1);
                case MarkCompressProtocol.TYPE_INT_2BYTE:
                    return readDoubleByNums(2);
                case MarkCompressProtocol.TYPE_INT_3BYTE:
                    return readDoubleByNums(3);
                case MarkCompressProtocol.TYPE_INT_4BYTE:
                    return readDoubleByNums(4);
                case MarkCompressProtocol.TYPE_INT_5BYTE:
                    return readDoubleByNums(5);
                case MarkCompressProtocol.TYPE_INT_6BYTE:
                    return readDoubleByNums(6);
                case MarkCompressProtocol.TYPE_INT_7BYTE:
                    return readDoubleByNums(7);
                case MarkCompressProtocol.TYPE_INT_8BYTE:
                    return readDoubleByNums(8);
                default:
                    throw ProtocolException.newTypeException(b);
            }
        }

        public override string readString()
        {
            int b = (int) mIn.getUint8();
            if (b == MarkCompressProtocol.TYPE_STRING)
            {
                int length = readInt();
                return mIn.readUTFBytes(length);
            }
            else if (b == MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE)
            {
                return null;
            }
            else
            {
                throw  ProtocolException.newTypeException(b);
            }
        }

        public override Byte readBytes()
        {
            throw new AbstractMethodException();
        }

        private float readFloatByNums(int bitNums)
        {
            mDoubleBuffer.length = 0;
            mDoubleBuffer.pos = 0;
            int i;
            int len = 4 - bitNums;
            if (mIn.endian == Byte.BIG_ENDIAN)
            {
                for (i = 0; i < len; i++)
                {
                    mDoubleBuffer.writeByte(0);
                }
                mDoubleBuffer.writeArrayBuffer(mIn.buffer, (uint) mIn.pos, (uint) bitNums);
            }
            else
            {
                mDoubleBuffer.writeArrayBuffer(mIn.buffer, (uint) mIn.pos, (uint) bitNums);
                mDoubleBuffer.pos = bitNums;
                for (i = 0; i < len; i++)
                {
                    mDoubleBuffer.writeByte(0);
                }
            }
            mDoubleBuffer.pos = 0;
            return mDoubleBuffer.getFloat32();
        }

        private double readDoubleByNums(int bitNums)
        {
            throw new AbstractMethodException();
        }


        private double getInt64Five()
        {
            int low;
            int high;
            if (mIn.endian == Byte.BIG_ENDIAN)
            {
                high = (int) mIn.getUint8();
                low = (int) mIn.getUint32();
            }
            else
            {
                low = (int) mIn.getUint32();
                high = (int) mIn.getUint8();
            }
            return high * 4294967296.0 + low;
        }

        private double getInt64Six()
        {
            int low;
            int high;
            if (mIn.endian == Byte.BIG_ENDIAN)
            {
                high = (int) mIn.getUint16();
                low = (int) mIn.getUint32();
            }
            else
            {
                low = (int) mIn.getUint32();
                high = (int) mIn.getUint16();
            }
            return high * 4294967296.0 + low;
        }

        /**7位*/
        private double getInt64Seven()
        {
            int low;
            int high;
            if (mIn.endian == Byte.BIG_ENDIAN)
            {
                high = (int) Utils.getThree(mIn);
                low = (int) mIn.getUint32();
            }
            else
            {
                low = (int) mIn.getUint32();
                high = (int) Utils.getThree(mIn);
            }
            return high * 4294967296.0 + low;
        }

        private double getInt64Long()
        {
            int low;
            int high;
            if (mIn.endian == Byte.BIG_ENDIAN)
            {
                high = (int) mIn.getUint32();
                low = (int) mIn.getUint32();
            }
            else
            {
                low = (int) mIn.getUint32();
                high = (int) mIn.getUint32();
            }
            if (high > 0x1fffff)
            {
                throw ProtocolException.newIn64ExceptionByHight((uint)high);
            }
            return high * 4294967296 + low;
        }
    }
}
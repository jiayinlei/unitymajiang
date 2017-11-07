using System;
using System.Collections.Generic;
using com.guojin.core.utils;

namespace com.guojin.core.io
{
    /*
     * 消息输出打包类
     */
    public class MarkCompressOutput : AbstractOutput
    {
            /**写入字符串的缓冲*/
            private Byte mWriteBuffer = new Byte();

            public static MarkCompressOutput create(Byte _out)
            {
                return new MarkCompressOutput(_out, MarkCompressProtocol.DEFAULT_CHARSET);
            }

        /// <summary>
        /// 通过字符集创建
        /// </summary>
        /// <param name="_out"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
            public static MarkCompressOutput createByCharset(Byte _out, string charset)
            {
                return new MarkCompressOutput(_out, charset);
            }

            public MarkCompressOutput(Byte _out, string charset) : base(_out, charset)
            {
                mWriteBuffer.endian = _out.endian;
            }
        public override void writeList<T>(Output _out, List<T> t)
        {
            if (t == null)
            {
                _out.writeInt(-1);
            }
            else
            {
                int Len = t.Count;
                _out.writeInt(Len);

                for (int i = 0; i < Len; i++)
                {
                    t[i].encode(_out);
                }
            }     
        }
        public override void writeT<T>(Output _out,T t)
        {
            if (t == null)
            {
                _out.writeBoolean(false);
            }
            else
            {
                _out.writeBoolean(true);
                t.encode(_out);
            }
        }
        public override void writeBoolean(bool v)
            {
                mOut.writeByte(v ? MarkCompressProtocol.TYPE_TRUE : MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE);
            }

            public override void writeInt(int v)
            {
                if (v > MarkCompressProtocol.TYPE_MIN - MarkCompressProtocol.TYPE_MINUS && v < MarkCompressProtocol.TYPE_MAX - MarkCompressProtocol.TYPE_MINUS)
                {
                    mOut.writeByte(v + MarkCompressProtocol.TYPE_MINUS);
                }
                else if (Utils.rightMove(v, 8) == 0)
                {
                    mOut.writeByte(MarkCompressProtocol.TYPE_INT_1BYTE);
                    mOut.writeByte(v);
                }
                else if (Utils.rightMove(v, 16) == 0)
                {
                    mOut.writeByte(MarkCompressProtocol.TYPE_INT_2BYTE);
                    mOut.writeInt16(v);
                }
                else if (Utils.rightMove(v, 24) == 0)
                {
                    mOut.writeByte(MarkCompressProtocol.TYPE_INT_3BYTE);
                    Utils.putThree(mOut, v);
                }
                else
                {
                    mOut.writeByte(MarkCompressProtocol.TYPE_INT_4BYTE);
                    mOut.writeInt32(v);
                }
            }

            public override void writeLong(long v)
            {
                //return new Int64(n, Math.floor(n / 4294967296.0));
                if (v < 0 || v != Math.Floor((double)v))
                {
                    throw  ProtocolException.newIn64Exception(v);
                }
                int low = (int) v;
                int high = Convert.ToInt32(v / 4294967296.0);
                if (high == 0 || ( v > MarkCompressProtocol.TYPE_MIN - MarkCompressProtocol.TYPE_MINUS && v < MarkCompressProtocol.TYPE_MAX - MarkCompressProtocol.TYPE_MINUS))
                {
                    writeInt(low);
                }
                else
                {
                    if (Utils.rightMove(high, 8) == 0)
                    {
                        mOut.writeByte(MarkCompressProtocol.TYPE_INT_5BYTE);
                        if (mOut.endian == Byte.BIG_ENDIAN)
                        {
                            mOut.writeByte(high);
                            mOut.writeInt32(low);
                        }
                        else
                        {
                            mOut.writeInt32(low);
                            mOut.writeByte(high);
                        }
                    }
                    else if (Utils.rightMove(high, 16) == 0)
                    {
                        mOut.writeByte(MarkCompressProtocol.TYPE_INT_6BYTE);
                        if (mOut.endian == Byte.BIG_ENDIAN)
                        {
                            mOut.writeInt16(high);
                            mOut.writeInt32(low);
                        }
                        else
                        {
                            mOut.writeInt32(low);
                            mOut.writeInt16(high);
                        }
                    }
                    else if (Utils.rightMove(high, 24) == 0)
                    {
                        mOut.writeByte(MarkCompressProtocol.TYPE_INT_7BYTE);
                        if (mOut.endian == Byte.BIG_ENDIAN)
                        {
                            Utils.putThree(mOut, high);
                            mOut.writeInt32(low);
                        }
                        else
                        {
                            mOut.writeInt32(low);
                            Utils.putThree(mOut, high);
                        }
                    }
                    else
                    {
                        mOut.writeByte(MarkCompressProtocol.TYPE_INT_8BYTE);
                        if (mOut.endian == Byte.BIG_ENDIAN)
                        {
                            mOut.writeInt32(high);
                            mOut.writeInt32(low);
                        }
                        else
                        {
                            mOut.writeInt32(low);
                            mOut.writeInt32(high);
                        }
                    }
                }
            }

            public override void writeFloat(float v)
            {
                if (v == 0)
                {
                    mOut.writeByte(MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE);
                    return;
                }
                mWriteBuffer.length = 0;
                mWriteBuffer.pos = 0;
                mWriteBuffer.writeFloat32(v);

                int freeNums = 0;
                int i;
                if (mOut.endian == Byte.BIG_ENDIAN)
                {
                    for (i = 0; i < mWriteBuffer.length; i++)
                    {
                        if (mWriteBuffer.buffer[i] == 0)
                        {
                            freeNums++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    mOut.writeByte(MarkCompressProtocol.TYPE_INT_1BYTE - (3 - freeNums));

                    mOut.writeArrayBuffer(mWriteBuffer.buffer, (uint) 0, (uint) freeNums);
                }
                else
                {
                    for (i = mWriteBuffer.length - 1; i >= 0; i--)
                    {
                        if (mWriteBuffer.buffer[i] == 0)
                        {
                            freeNums++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    mOut.writeByte(MarkCompressProtocol.TYPE_INT_1BYTE - (3 - freeNums));

                    mOut.writeArrayBuffer(mWriteBuffer.buffer, (uint) 0, (uint) (4 - freeNums));
                }
            }

            public override void writeDouble(double v)
            {
                throw new System.Exception("AbstractMethodException");
            }

            public override void writeString(string s)
            {
                Console.WriteLine("writeString:{0}", s);
                if (s == null)
                {
                    mOut.writeByte(MarkCompressProtocol.TYPE_NULL_OR_ZERO_OR_FALSE);
                }
                else
                {
                    mOut.writeByte(MarkCompressProtocol.TYPE_STRING);
                    mWriteBuffer.length = 0;
                    mWriteBuffer.pos = 0;
                    mWriteBuffer.writeUTFBytes(s);
                    Console.WriteLine("mWriteBuffer.length:{0}", mWriteBuffer.length);
                    writeInt(mWriteBuffer.length);

                    mOut.writeArrayBuffer(mWriteBuffer.buffer, (uint) 0, (uint) mWriteBuffer.length);
                }
            }

            public override void writeBytes(Byte bs)
            {
                throw new System.Exception("AbstractMethodException");
            }
    }
}
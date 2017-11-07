using System;
using System.Collections.Generic;
using com.guojin.core.utils;
using System.Text;

namespace com.guojin.core.io
{
    /**
     *
     * <code>Byte</code> 类提供用于优化读取、写入以及处理二进制数据的方法和属性。
     */
    public class Byte {
        /*[DISABLE-ADD-VARIABLE-DEFAULT-VALUE]*/
        /**
         * 表示多字节数字的最高有效字节位于字节序列的最前面。
         */
        public static string BIG_ENDIAN = "bigEndian";
        /**
         * 表示多字节数字的最低有效字节位于字节序列的最前面。
         */
        public static string LITTLE_ENDIAN = "littleEndian";
        /**
         * @private
         * 是否为小端数据。
         */
        protected bool _xd_ = true;
        private int _allocated_ = 8;
        /**
         * @private
         * 原始数据。
         */
        protected List<byte> _d_;
        /**
         * @private
         * DataView
         */
        protected List<byte> _u8d_;
        /**@private */
        protected int _pos_ = 0;
        /**@private */
        protected int _length = 0;
        /**@private */
        private static string _sysEndian = null;
        
        /**
         * 获取系统的字节存储顺序。
         * @return 字节存储顺序。
         */
        public static string getSystemEndian() {
            if (_sysEndian == null) {
                const ushort value = 0xFF00;  
                byte[] bytes = BitConverter.GetBytes(value);  

                if (bytes[0] == 0x00 && bytes[1] == 0xFF) {
                    _sysEndian = Byte.LITTLE_ENDIAN;
                }
                else if (bytes[0] == 0xFF && bytes[1] == 0x00) {
                    _sysEndian = Byte.BIG_ENDIAN;
                } else {
                    throw new ArithmeticException(
                        "Error occurs while judge system endian.");
                }

            }
            return _sysEndian;
        }
        
        /**
         * 创建一个 <code>Byte</code> 类的实例。
         * @param	data 用于指定元素的数目、类型化数组、ArrayBuffer。
         */
        public Byte(List<byte> data = null) {
            Console.WriteLine("Byte constructed.");
            if (data != null) {
                Console.WriteLine("Byte DATA is NULL.");
                this._d_ = data;
                _length = this._d_.Count;
            } else {
                Console.WriteLine("Byte buffer allocated is {0}", _allocated_);
                this.___resizeBuffer(this._allocated_);
            }
        
        }
        
        /**
         * 获取此对象的 ArrayBuffer数据,数据只包含有效数据部分 。
         */
        public List<byte> buffer {
            get {
                return this._d_;
            }
        }
        
        /**
         * 字节顺序。
         */
        public string endian {
            get {
                return _xd_ ? LITTLE_ENDIAN : BIG_ENDIAN;
            }
            set {
                _xd_ = (value == LITTLE_ENDIAN);
            }
        }
        
        /**
         * 字节长度。
         */		
        public int length {
            get { return _d_.Count; }
            set {
                Console.WriteLine("allocated:{0}, value {1}", _allocated_, value);
                /*if (_allocated_ < value) {
                    double max = (double) Convert.ToDouble(Math.Max(value, _allocated_ * 2));
                    ___resizeBuffer(_allocated_ = (int) Math.Floor(max));
                } else if (_allocated_ > value)
                    ___resizeBuffer(_allocated_ = value);
                _length = value;*/
                if (value == 0) {
                    ___resizeBuffer(0);
                }
            }

        }
        
        /** @private */
        private void ___resizeBuffer(int len) {
            try {
                this._d_ = new List<byte>(len);
                Console.WriteLine("resizeBuffer called, len {0}", len);
            } catch (System.Exception) {
                throw new System.Exception("___resizeBuffer err:" + len);
            }
        }
        
        /**
         * 读取字符型值。
         * @return
         */
        public string getString() {
            return rUTF(getUint16());
        }
        
        //LITTLE_ENDIAN only now;
        /**
         * 从指定的位置读取指定长度的数据用于创建一个 Float32Array 对象并返回此对象。
         * @param	start 开始位置。
         * @param	len 需要读取的字节长度。
         * @return  读出的 Float32Array 对象。
         */
        public float getFloat32Array(int start, int len) {
            return (float) 0;
        }
        
        /**
         * 从指定的位置读取指定长度的数据用于创建一个 Uint8Array 对象并返回此对象。
         * @param	start 开始位置。
         * @param	len 需要读取的字节长度。
         * @return  读出的 Uint8Array 对象。
         */
        public byte[] getUint8Array(int start, int len) {
            byte[] v = _d_.GetRange(start, len).ToArray();
            _pos_ += len;
            return v;
        }
        
        /**
         * 从指定的位置读取指定长度的数据用于创建一个 Int16Array 对象并返回此对象。
         * @param	start 开始位置。
         * @param	len 需要读取的字节长度。
         * @return  读出的 Uint8Array 对象。
         */
        public int[] getInt16Array(int start, int len) {
            return new int[]{};
        }
        
        /**
         * 在指定字节偏移量位置处读取 Float32 值。
         * @return Float32 值。
         */
        public float getFloat32() {
            return (float) 0;
        }
        
        public double getFloat64() {
            return (double) 0;
        }
        
        /**
         * 在当前字节偏移量位置处写入 Float32 值。
         * @param	value 需要写入的 Float32 值。
         */
        public void writeFloat32(float value) {
            
        }
        
        public void writeFloat64(double value) {
            
        }
        public int getInt32()
        {
            int b1 = _d_[this._pos_]; string b1String = Convert.ToString(b1, 16);
            int b2 = _d_[this._pos_ + 1]; string b2String = Convert.ToString(b2, 16);
            int b3 = _d_[this._pos_ + 2]; string b3String = Convert.ToString(b3, 16);
            int b4 = _d_[this._pos_ + 3]; string b4String = Convert.ToString(b4, 16);
            if (b1String.Length < 2)
            {
                b1String = "0" + b1String;
            }

            if (b2String.Length < 2)
            {
                b2String = "0" + b2String;
            }

            if (b3String.Length < 2)
            {
                b3String = "0" + b3String;
            }

            if (b4String.Length < 2)
            {
                b4String = "0" + b4String;
            }


            //int b1 = _d_[this._pos_]; string b1String = Convert.ToString(b1,2);
            //int b2 = _d_[this._pos_ + 1]; string b2String = Convert.ToString(b2,2);
            //int b3 = _d_[this._pos_ + 2]; string b3String = Convert.ToString(b3,2);
            //int b4 = _d_[this._pos_ + 3]; string b4String = Convert.ToString(b4,2);

            // todo 4字节二进制转十进制
            string str = "";
            if (_xd_)
            {
                str = b4String + b3String + b2String + b1String;
            }
            else
            {
                str = b1String + b2String + b3String + b4String;
            }
            _pos_ += 4;
            int v = Convert.ToInt32(str, 16);
            return v;
            /*
            int ui;
            if (_xd_)
            {
                ui = b4 << 24 & b3 << 16 & b2 << 8 & b1;
            }
            else
            {
                ui = b1 << 24 & b2 << 16 & b3 << 8 & b4;
            }
            _pos_ += 4;
            return ui;
            */
        }
        /**
         * 在当前字节偏移量位置处读取 Int32 值。
         * @return Int32 值。
         */
        //public int getInt32() {
        //    int b1 = _d_[this._pos_]; string b1String = Convert.ToString(b1, 2);
        //    int b2 = _d_[this._pos_ + 1]; string b2String = Convert.ToString(b2, 2);
        //    int b3 = _d_[this._pos_ + 2]; string b3String = Convert.ToString(b3, 2);
        //    int b4 = _d_[this._pos_ + 3]; string b4String = Convert.ToString(b4, 2);

        //    // todo 4字节二进制转十进制
        //    string str = "";
        //    if (_xd_)
        //    {
        //        str = b4String + b3String + b2String + b1String;
        //    }
        //    else
        //    {
        //        str = b1String + b2String + b3String + b4String;
        //    }
        //    _pos_ += 4;
        //    int v = Convert.ToInt32(str, 2);
        //    return v;
        //    /*
        //    int ui;
        //    if (_xd_)
        //    {
        //        ui = b4 << 24 & b3 << 16 & b2 << 8 & b1;
        //    }
        //    else
        //    {
        //        ui = b1 << 24 & b2 << 16 & b3 << 8 & b4;
        //    }
        //    _pos_ += 4;
        //    return ui;
        //    */
        //}

        /**
         * 在当前字节偏移量位置处读取 Uint32 值。
         * @return Uint32 值。
         */
        public uint getUint32() {
            uint b1 = _d_[this._pos_];
            uint b2 = _d_[this._pos_ + 1];
            uint b3 = _d_[this._pos_ + 2];
            uint b4 = _d_[this._pos_ + 3];
            uint ui;
            if (_xd_) {
                ui = b4 << 24 & b3 << 16 & b2 << 8 & b1;
            } else {
                ui = b1 << 24 & b2 << 16 & b3 << 8 & b4;
            }
            _pos_ += 4;
            return ui;
        }
        
        /**
         * 在当前字节偏移量位置处写入 Int32 值。
         * @param	value 需要写入的 Int32 值。
         */
        public void writeInt32(int value) {
            ensureWrite(this._pos_ + 4);
            if (_xd_) {
                _d_.Insert(this._pos_, Convert.ToByte(value & 0x000000FF));
                _d_.Insert(this._pos_ + 1, Convert.ToByte(value >> 8 & 0x000000FF));
                _d_.Insert(this._pos_ + 2, Convert.ToByte(value >> 16 & 0x000000FF));
                _d_.Insert(this._pos_ + 3, Convert.ToByte(value >> 24 & 0x000000FF));
            } else {
                _d_.Insert(this._pos_, Convert.ToByte(value >> 24 & 0x000000FF));
                _d_.Insert(this._pos_ + 1, Convert.ToByte(value >> 16 & 0x000000FF));
                _d_.Insert(this._pos_ + 2, Convert.ToByte(value >> 8 & 0x000000FF));
                _d_.Insert(this._pos_ + 3, Convert.ToByte(value & 0x000000FF));
            }
            _pos_ += 4;
        }
        
        /**
         * 在当前字节偏移量位置处写入 Uint32 值。
         * @param	value 需要写入的 Uint32 值。
         */
        public void writeUint32(int value) {
            ensureWrite(this._pos_ + 4);
            if (_xd_) {
                _d_.Insert(this._pos_, Convert.ToByte(value & 0x000000FF));
                _d_.Insert(this._pos_ + 1, Convert.ToByte(Utils.rightMove(value, 8) & 0x000000FF));
                _d_.Insert(this._pos_ + 2, Convert.ToByte(Utils.rightMove(value, 16) & 0x000000FF));
                _d_.Insert(this._pos_ + 3, Convert.ToByte(Utils.rightMove(value, 24) & 0x000000FF));
            } else {
                _d_.Insert(this._pos_, Convert.ToByte(Utils.rightMove(value, 24) & 0x000000FF));
                _d_.Insert(this._pos_ + 1, Convert.ToByte(Utils.rightMove(value, 16) & 0x000000FF));
                _d_.Insert(this._pos_ + 2, Convert.ToByte(Utils.rightMove(value, 8) & 0x000000FF));
                _d_.Insert(this._pos_ + 3, Convert.ToByte(value & 0x000000FF));
            }
            _pos_ += 4;
        }
        
        /**
         * 在当前字节偏移量位置处读取 Int16 值。
         * @return Int16 值。
         */
        public int getInt16() {
            int high = _d_[this._pos_];
            int low = _d_[this._pos_ + 1];
            int us;
            if (_xd_) {
                us = high << 8 & low;
            } else {
                us = low << 8 & high;
            }
            this._pos_ += 2;
            return us;          
        }
        
        /**
         * 在当前字节偏移量位置处读取 Uint16 值。
         * @return Uint16 值。
         */
        public uint getUint16() {
            return (uint) _getUint16(_pos_);
        }
        
        /**
         * 在当前字节偏移量位置处写入 Uint16 值。
         * @param	value 需要写入的Uint16 值。
         */
        public void writeUint16(int value) {
            ensureWrite(this._pos_ + 2);
            if (_xd_) {
                _d_.Insert(this._pos_, Convert.ToByte(value & 0x00FF));
                _d_.Insert(this._pos_ + 1, Convert.ToByte(value >> 8));
            } else {
                _d_.Insert(this._pos_, Convert.ToByte(Utils.rightMove(value, 8)));
                _d_.Insert(this._pos_ + 1, Convert.ToByte(value & 0x00FF));
            }
            _pos_ += 2;
        }
        
        /**
         * 在当前字节偏移量位置处写入 Int16 值。
         * @param	value 需要写入的 Int16 值。
         */
        public void writeInt16(int value) {
            ensureWrite(this._pos_ + 2);
            //_d_.setInt16(_pos_, value, _xd_);
            if (_xd_) {
                _d_.Insert(this._pos_, Convert.ToByte(value & 0x00FF));
                _d_.Insert(this._pos_ + 1, Convert.ToByte(value >> 8));
            } else {
                _d_.Insert(this._pos_, Convert.ToByte(Utils.rightMove(value, 8)));
                _d_.Insert(this._pos_ + 1, Convert.ToByte(value & 0x00FF));
            }
            _pos_ += 2;
        }
        
        /**
         * 在当前字节偏移量位置处读取 Uint8 值。
         * @return Uint8 值。
         */
        public byte getUint8() {
            //return _d_.getUint8(_pos_++);
            return _d_[_pos_++];
        }
        
        /**
         * 在当前字节偏移量位置处写入 Uint8 值。
         * @param	value 需要写入的 Uint8 值。
         */
        public void writeUint8(byte value) {
            ensureWrite(this._pos_ + 1);
            //_d_.setUint8(_pos_, value, _xd_);
            _d_.Insert(_pos_, value);
            _pos_++;
        }
        
        /**
         * @private
         * 在指定位置处读取 Uint8 值。
         * @param	pos 字节读取位置。
         * @return Uint8 值。
         */
        public byte _getUInt8(int pos) {
            //return _d_.getUint8(pos);
            return _d_[pos];
        }
        
        /**
         * @private
         * 在指定位置处读取 Uint16 值。
         * @param	pos 字节读取位置。
         * @return Uint16 值。
         */
        public uint _getUint16(int pos) {
            uint high = _d_[this._pos_];
            uint low = _d_[this._pos_ + 1];
            uint us;
            if (_xd_) {
                us = high << 8 & low;
            } else {
                //us = low << 8 & high;
                us = high * 256 + low;
            }
            this._pos_ += 2;
            return us;        
        }
        
        /**
         * @private
         * 使用 getFloat32() 读取6个值，用于创建并返回一个 Matrix 对象。
         * @return  Matrix 对象。
         */
        public void _getMatrix() {
        }

        /**
         * @private
         * 读取指定长度的 UTF 型字符串。
         * @param	len 需要读取的长度。
         * @return 读出的字符串。
         */
        //private string rUTF(uint len) {
        //    string v = "";
        //    int max = this._pos_ + Convert.ToInt32(len);
        //    int c;
        //    int c2;
        //    int c3;
        //    List<byte> u = this._d_;
        //    int i = 0;
        //    while (_pos_ < max) {
        //        c = u[_pos_++];
        //        if (c < 0x80) {
        //            if (c != 0) {
        //                v += Encoding.GetEncoding(MarkCompressProtocol.DEFAULT_CHARSET).GetString(new byte[] { (byte) c });
        //            }
        //        } else if (c < 0xE0) {
        //            v += Convert.ToString(((c & 0x3F) << 6) | (u[_pos_++] & 0x7F));
        //        } else if (c < 0xF0) {
        //            c2 = u[_pos_++];
        //            v += Convert.ToString(((c & 0x1F) << 12) | ((c2 & 0x7F) << 6) | (u[_pos_++] & 0x7F));
        //        } else {
        //            c2 = u[_pos_++];
        //            c3 = u[_pos_++];
        //            v += Convert.ToString(((c & 0x0F) << 18) | ((c2 & 0x7F) << 12) | ((c3 << 6) & 0x7F) | (u[_pos_++] & 0x7F));
        //        }
        //        i++;
        //    }
        //    return v;
        //}
        //王总修复中文
        private string rUTF(uint len)
        {
            string v = "";
            int max = this._pos_ + Convert.ToInt32(len);
            int c;
            List<byte> u = this._d_;
            int i = 0;
            while (_pos_ < max)
            {
                c = u[_pos_++];
                if (c < 0x80)//128
                {
                    if (c != 0)
                    {
                        v += System.Text.Encoding.UTF8.GetString(new byte[] { (byte)c });
                    }
                }
                else if (c < 0xE0)//224
                {
                    //v += Convert.ToString(((c & 0x3F) << 6) | (u[_pos_++] & 0x7F));
                    v += System.Text.Encoding.UTF8.GetString(new byte[] { (byte)c, (byte)u[_pos_++] });
                }
                else if (c < 0xF0)//240
                {
                    //c2 = u[_pos_++];
                    //v += Convert.ToString(((c & 0x1F) << 12) | ((c2 & 0x7F) << 6) | (u[_pos_++] & 0x7F));
                    v += System.Text.Encoding.UTF8.GetString(new byte[] { (byte)c, (byte)u[_pos_++], (byte)u[_pos_++] });
                }
                else
                {
                    //c2 = u[_pos_++];
                    //c3 = u[_pos_++];
                    v += System.Text.Encoding.UTF8.GetString(new byte[] { (byte)c, (byte)u[_pos_++], (byte)u[_pos_++], (byte)u[_pos_++] });
                }
                i++;
            }
            return v;
        }

        // River: 自定义的字符串读取,项目相关的内容
        /**
         * 字符串读取。
         * @param	len
         * @return
         */
        public string getCustomString(int len) {
            return null;
        }
        
        /**
         * 当前读取到的位置。
         */
        public int  pos {
            get { return _pos_; }
            set {
                _pos_ = value;
            }
        }
        
        /**
         * 可从字节流的当前位置到末尾读取的数据的字节数。
         */
        public int bytesAvailable {
            get { return length - _pos_;}
        }
        
        /**
         * 清除数据。
         */
        public void clear() {
            
            _pos_ = 0;
            length = 0;
        }
        
        /**
         * @private
         * 获取此对象的 ArrayBuffer 引用。
         * @return
         */
        public List<byte> __getBuffer() {
            //this._d_.buffer.byteLength = this.length;
            return _d_;
        }
        
        /**
         * 写入字符串，该方法写的字符串要使用 readUTFBytes 方法读取。
         * @param value 要写入的字符串。
         */
        public void writeUTFBytes(string value) {
            // utf8-decode
            value = value + "";
            int sz = value.Length;
            for (int i = 0; i < sz; i++) {


                    
                byte[] b  = System.Text.Encoding.UTF8.GetBytes(value[i].ToString());
                for (int j = 0; j < b.Length; j++)
                {
                    int c = (int)(b[j]);
                    writeByte(c);

                    /*
                    int c = (int)(b[j]);
                    //int c = 0;
                    if (c <= 0x7F)
                    {
                        writeByte(c);
                    }
                    else if (c <= 0x7FF)
                    {
                        //这里要优化,胡高，writeShort,后面也是
                        writeByte(0xC0 | (c >> 6));//   11000000
                        writeByte(0x80 | (c & 63));//   10000000

                    }
                    else if (c <= 0xFFFF)
                    {

                        writeByte(0xE0 | (c >> 12));//   11100000
                        writeByte(0x80 | ((c >> 6) & 63));//
                        writeByte(0x80 | (c & 63));

                    }
                    else
                    {
                        writeByte(0xF0 | (c >> 18));//  11110000
                        writeByte(0x80 | ((c >> 12) & 63));
                        writeByte(0x80 | ((c >> 6) & 63));
                        writeByte(0x80 | (c & 63));
                    }
                    */
                }
                // int c = Convert.ToByte(value[i]);




            }
        }
        
        /**
         * 将 UTF-8 字符串写入字节流。
         * @param	value 要写入的字符串值。
         */
        public void writeUTFString(string value) {
            
        }
        
        /**
         * @private
         * 读取 UTF-8 字符串。
         * @return 读出的字符串。
         */
        public string readUTFString() {
            int tPos;
            tPos = pos;
            int len;
            len = Convert.ToInt32(getUint16());
            //trace("readLen:"+len,"pos,",tPos);
            return readUTFBytes(len);
        }
        
        /**
         * 读取 UTF-8 字符串。
         * @return 读出的字符串。
         */
        public string getUTFString() {
            return readUTFString();
        }
        
        /**
         * @private
         * 读字符串，必须是 writeUTFBytes 方法写入的字符串。
         * @param len 要读的buffer长度,默认将读取缓冲区全部数据。
         * @return 读取的字符串。
         */
        public string readUTFBytes(int len = -1) {
            if(len==0) return "";
            uint _len = (uint) (len > 0 ? len : bytesAvailable);
            return rUTF(_len);
        }
        
        /**
         * 读字符串，必须是 writeUTFBytes 方法写入的字符串。
         * @param len 要读的buffer长度,默认将读取缓冲区全部数据。
         * @return 读取的字符串。
         */
        public string getUTFBytes(int len = -1) {
            return readUTFBytes(len);
        }
        
        /**
         * 在字节流中写入一个字节。
         * @param	value
         */
        public void writeByte(int value) {
            ensureWrite(this._pos_ + 1);
            _d_.Insert(this._pos_,(byte)(value &0xff) );//Convert.ToByte()
            //_d_.Add(Convert.ToByte(value));
            Console.WriteLine("-----------------");
            Console.WriteLine("Raw:{0}, pos:{1}", Utils.debug(_d_), _pos_);        
            this._pos_ += 1;
        }
        
        /**
         * @private
         * 在字节流中读一个字节。
         */
        public int readByte() {
            return (int) _d_[_pos_++];
        }
        
        /**
         * 在字节流中读一个字节。
         */
        public int getByte() {
            return readByte();
        }
        
        /**
         * 指定该字节流的长度。
         * @param	lengthToEnsure 指定的长度。
         */
        public void ensureWrite(int lengthToEnsure) {
            //if (this._length < lengthToEnsure) this._length = lengthToEnsure;
            //if (this._allocated_ < lengthToEnsure) length = lengthToEnsure;
        }
        
        /**
         * 写入指定的 Arraybuffer 对象。
         * @param	arraybuffer 需要写入的 Arraybuffer 对象。
         * @param	offset 偏移量（以字节为单位）
         * @param	length 长度（以字节为单位）
         */
        public void writeArrayBuffer(List<byte> arraybuffer, uint offset = 0, uint length = 0) {
            if (offset < 0 || length < 0) throw new System.Exception("writeArrayBuffer error - Out of bounds");
            if (length == 0) length = (uint) arraybuffer.Count - offset;
            ensureWrite(this._pos_ + (int) length);
            this._d_.InsertRange(_pos_, arraybuffer);
            this._pos_ += (int) length;
        }
    }
}
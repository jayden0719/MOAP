using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MOAP
{
    public class Header : ISerializable
    {
        public uint MOAID { get; set; }
        public uint MOATYPE { get; set; }
        public uint BODYLEN { get; set; }
        public byte FRAGMENTED { get; set; }
        public byte LASTMSG { get; set; }
        public ushort SEQ { get; set; }

        public Header()
        {

        }
        public Header(byte[] bytes)
        {
            MOAID = BitConverter.ToUInt32(bytes, 0);
            MOATYPE = BitConverter.ToUInt32(bytes, 4);
            BODYLEN = BitConverter.ToUInt32(bytes, 8);
            FRAGMENTED = bytes[12];
            LASTMSG = bytes[13];
            SEQ = BitConverter.ToUInt16(bytes, 14);
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[16];

            byte[] temp = BitConverter.GetBytes(MOAID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);

            temp = BitConverter.GetBytes(MOATYPE);
            Array.Copy(temp, 0, bytes, 4, temp.Length);

            temp = BitConverter.GetBytes(BODYLEN);
            Array.Copy(temp, 0, bytes, 8, temp.Length);

            bytes[12] = FRAGMENTED;
            bytes[13] = LASTMSG;

            temp = BitConverter.GetBytes(SEQ);
            Array.Copy(temp, 0, bytes, 14, temp.Length);

            return bytes;
        }

        public int GetSize()
        {
            return 16;
        }
    }
}

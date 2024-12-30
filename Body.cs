using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOAP
{
    //파일전송 요청
    public class BodyRequest : ISerializable
    {
        public long FILESIZE;
        public byte[] FILENAME;

        public BodyRequest(){ }

        public BodyRequest(byte[] bytes)
        {
            FILESIZE = BitConverter.ToInt64(bytes, 0);
            FILENAME = new byte[bytes.Length - sizeof(long)];
            Array.Copy(bytes, sizeof(long), FILENAME, 0, FILENAME.Length);
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(FILESIZE);
            Array.Copy(temp, 0, bytes, 0, temp.Length); //bytes객체에 파일 사이즈 데이터를 0번 인덱스부터 저장
            Array.Copy(FILENAME, 0, bytes, temp.Length, FILENAME.Length); // 파일이름을 파일사이즈 이후 인덱스에 저장

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(long) + FILENAME.Length;
        }
    }

    //문자전송결과 요청
    public class MsgBodyRequest : ISerializable
    {
        public byte[] USERID, REQDATE, ENDDATE, SERVICE;

        public MsgBodyRequest() { }

        public MsgBodyRequest(byte[] bytes) // 저장된 바이트 배열에서 추출 방식
        {
            USERID = new byte[bytes.Length];
            REQDATE = new byte[bytes.Length];
            ENDDATE = new byte[bytes.Length];
            SERVICE = new byte[bytes.Length];

            Array.Copy(bytes, 0, REQDATE, 0, 10);
            Array.Copy(bytes, 10, ENDDATE, 0, 10);
            Array.Copy(bytes, 10 + 10, SERVICE, 0, 3);
            Array.Copy(bytes, 10 + 10 + 3, USERID, 0, bytes.Length - (10 + 10 + 3));

        }

        public byte[] GetBytes() // 바이트 배열 저장 방식
        {
            byte[] bytes = new byte[GetSize()];          
            Array.Copy(REQDATE, 0, bytes, 0, REQDATE.Length);
            Array.Copy(ENDDATE, 0, bytes, REQDATE.Length, ENDDATE.Length);
            Array.Copy(SERVICE, 0, bytes, REQDATE.Length + ENDDATE.Length, SERVICE.Length);
            Array.Copy(USERID, 0, bytes, REQDATE.Length + ENDDATE.Length + SERVICE.Length, USERID.Length);
            return bytes;
        }

        public int GetSize()
        {
            return USERID.Length + REQDATE.Length + ENDDATE.Length + SERVICE.Length;
        }
    }

    //팩스전송결과 요청
    public class FAXBodyRequest : ISerializable
    {
        public byte[] USERID, REQDATE, ENDDATE, SERVICE;

        public FAXBodyRequest() { }

        public FAXBodyRequest(byte[] bytes)
        {
            USERID = new byte[bytes.Length - sizeof(int)];
            REQDATE = new byte[bytes.Length - sizeof(int)];
            ENDDATE = new byte[bytes.Length - sizeof(int)];
            SERVICE = new byte[bytes.Length - sizeof(int)];

            Array.Copy(bytes, 0, REQDATE, 0, 10);
            Array.Copy(bytes, 10, ENDDATE, 0, 10);
            Array.Copy(bytes, 10 + 10, SERVICE, 0, 3);
            Array.Copy(bytes, 10 + 10 + 3, USERID, 0, bytes.Length - (10 + 10 + 3));
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            Array.Copy(REQDATE, 0, bytes, 0, REQDATE.Length);
            Array.Copy(ENDDATE, 0, bytes, REQDATE.Length, ENDDATE.Length);
            Array.Copy(SERVICE, 0, bytes, REQDATE.Length + ENDDATE.Length, SERVICE.Length);
            Array.Copy(USERID, 0, bytes, REQDATE.Length + ENDDATE.Length + SERVICE.Length, USERID.Length);
            return bytes;
        }

        public int GetSize()
        {
            return USERID.Length + REQDATE.Length + ENDDATE.Length + SERVICE.Length;
        }
    }


    //파일전송 응답
    public class BodyResponse : ISerializable
    {
        public uint MOAID;
        public byte RESPONSE;
        public BodyResponse()
        {

        }
        public BodyResponse(byte[] bytes)
        {
            MOAID = BitConverter.ToUInt32(bytes, 0);
            RESPONSE = bytes[4];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(MOAID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            bytes[temp.Length] = RESPONSE;

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(uint) + sizeof(byte);
        }
    }
    
    //파일전송 데이터
    public class BodyData : ISerializable
    {
        public byte[] DATA;
        public BodyData(byte[] bytes)
        {
            DATA = new byte[bytes.Length];
            bytes.CopyTo(DATA, 0);
        }

        public byte[] GetBytes()
        {
            return DATA;
        }

        public int GetSize()
        {
            return DATA.Length;
        }
    }

    //문자전송 데이터
    public class MSGBodyData : ISerializable
    {
        public byte[] DATA;
        public MSGBodyData(byte[] bytes)
        {
            DATA = new byte[bytes.Length];
            bytes.CopyTo(DATA, 0);
        }

        public byte[] GetBytes()
        {
            return DATA;
        }

        public int GetSize()
        {
            return DATA.Length;
        }
    }

    //파일전송 데이터
    public class FAXBodyData : ISerializable
    {
        public byte[] DATA;
        public FAXBodyData(byte[] bytes)
        {
            DATA = new byte[bytes.Length];
            bytes.CopyTo(DATA, 0);
        }

        public byte[] GetBytes()
        {
            return DATA;
        }

        public int GetSize()
        {
            return DATA.Length;
        }
    }


    //파일전송 결과
    public class BodyResult : ISerializable
    {
        public uint MOAID;
        public byte RESULT;

        public BodyResult() { }
        public BodyResult(byte[] bytes)
        {
            MOAID = BitConverter.ToUInt32(bytes, 0);
            RESULT = bytes[4];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(MOAID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            bytes[temp.Length] = RESULT;

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(uint) + sizeof(byte);
        }
    }

    //문자전송 결과
    public class MSGBodyResult : ISerializable
    {
        public uint MOAID;
        public byte RESULT;

        public MSGBodyResult() { }
        public MSGBodyResult(byte[] bytes)
        {
            MOAID = BitConverter.ToUInt32(bytes, 0);
            RESULT = bytes[4];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(MOAID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            bytes[temp.Length] = RESULT;

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(uint) + sizeof(byte);
        }
    }

    //팩스전송 결과
    public class FAXBodyResult : ISerializable
    {
        public uint MOAID;
        public byte RESULT;

        public FAXBodyResult() { }
        public FAXBodyResult(byte[] bytes)
        {
            MOAID = BitConverter.ToUInt32(bytes, 0);
            RESULT = bytes[4];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(MOAID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            bytes[temp.Length] = RESULT;

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(uint) + sizeof(byte);
        }
    }
}

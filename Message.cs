namespace MOAP
{
    public class CONSTANTS
    {
        // MOATYPE 상수 정의와 SWITCH문 레이블 값 구분
        public const uint REQ_MSG_SEND = 0x01;
        public const uint MSG_SEND_DATA = 0x02;
        public const uint MSG_SEND_RES = 0x03;

        public const uint REQ_FAX_SEND = 0x04;
        public const uint FAX_SEND_DATA = 0x05;
        public const uint FAX_SEND_RES = 0x06;

        public const uint REQ_FILE_SEND = 0x07;
        public const uint REP_FILE_SEND = 0x08;
        public const uint FILE_SEND_DATA = 0x09;
        public const uint FILE_SEND_RES = 0x10;

        public const byte NOT_FRAGMENTED = 0x00;
        public const byte FRAGMENTED = 0x01;

        public const byte NOT_LASTMSG = 0x00;
        public const byte LASTMSG = 0x01;

        public const byte ACCTEPTED = 0x01;
        public const byte DENIED = 0x00;

        public const byte FAIL = 0x00;
        public const byte SUCCESS = 0x01;
    }

    public interface ISerializable //인터페이스
    {
        byte[] GetBytes();
        int GetSize();
    }


    public class Message : ISerializable //헤더 크기와 바디 크기의 총합 바이트 배열에 헤더와 바디를 저장하여 반환한다
    {
        public Header Header { get; set; }
        public ISerializable Body { get; set; }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            Header.GetBytes().CopyTo(bytes, 0);
            Body.GetBytes().CopyTo(bytes, Header.GetSize());

            return bytes;
        }

        public int GetSize()
        {
            return Header.GetSize() + Body.GetSize();
        }
    }   
}

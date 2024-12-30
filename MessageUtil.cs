using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOAP
{
    public class MessageUtil
    {
        //전송 메소드 
        public static void Send(Stream writer, Message msg)
        {
            writer.Write(msg.GetBytes(), 0, msg.GetSize());
        }

        //수신 메소드
        public static Message Receive(Stream reader)
        {
            int totalRecv = 0;
            int sizeToRead = 16;
            byte[] hBuffer = new byte[sizeToRead];

            while (sizeToRead > 0)
            {
                byte[] buffer = new byte[sizeToRead];
                int recv = reader.Read(buffer, 0, sizeToRead);
                if (recv == 0)
                {
                    return null;
                }
                buffer.CopyTo(hBuffer, totalRecv); //totalRecv는 hBuffer의 인덱스 역할을 해준다
                totalRecv += recv;
                sizeToRead -= recv;
            }

            Header header = new Header(hBuffer);
            totalRecv = 0;
            byte[] bBuffer = new byte[header.BODYLEN]; //바디 버퍼의 크기를 지정해준다
            sizeToRead = (int)header.BODYLEN;

            while (sizeToRead > 0)
            {
                byte[] buffer = new byte[sizeToRead];
                int recv = reader.Read(buffer, 0, sizeToRead);
                if (recv == 0)
                {
                    return null;
                }
                buffer.CopyTo(bBuffer, totalRecv);
                totalRecv += recv;
                sizeToRead -= recv;
            }

            ISerializable body = null;
            switch (header.MOATYPE)
            {
                case CONSTANTS.REQ_FILE_SEND:
                    body = new BodyRequest(bBuffer);
                    break;
                case CONSTANTS.REP_FILE_SEND:
                    body = new BodyResponse(bBuffer);
                    break;
                case CONSTANTS.FILE_SEND_DATA:
                    body = new BodyData(bBuffer);
                    break;
                case CONSTANTS.FILE_SEND_RES:
                    body = new BodyResult(bBuffer);
                    break;
                case CONSTANTS.REQ_MSG_SEND:
                    body = new MsgBodyRequest(bBuffer);
                    break;               
                case CONSTANTS.MSG_SEND_DATA:
                    body = new MSGBodyData(bBuffer);
                    break;
                case CONSTANTS.MSG_SEND_RES:
                    body = new MSGBodyResult(bBuffer);
                    break;
                case CONSTANTS.REQ_FAX_SEND:
                    body = new FAXBodyRequest(bBuffer);
                    break;
                case CONSTANTS.FAX_SEND_DATA:
                    body = new FAXBodyData(bBuffer);
                    break;
                case CONSTANTS.FAX_SEND_RES:
                    body = new FAXBodyResult(bBuffer);
                    break;
                default:
                    throw new Exception(
                        string.Format("Unknown MOATYPE : {0}", header.MOATYPE));
            }
           
            return new Message() { Header = header, Body = body };
        }
    }
}

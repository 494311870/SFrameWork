using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SServer.Common.Specification;
using System.Net.Sockets;
using SServer.Common.Tools;
using System;
using System.Threading.Tasks;

/// <summary>
/// 对等客户端
/// 用来保持和客户端建立的连接
/// </summary>
public class Client : IClient
{
    private readonly string ip = "127.0.0.1";
    private readonly int port = 9966;

    public TcpClient TcpClient { get; } = new TcpClient();

    private Message Message { get; } = new Message();
    private NetworkStream Stream { get; set; }



    void OnInit()
    {
        try
        {
            TcpClient.ConnectAsync(ip, port);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"无法连接到服务器：{e}");
        }
    }

    void OnDestroy()
    {
        try
        {
            TcpClient.Close();
        }
        catch (Exception e)
        {

            Debug.LogWarning($"无法关闭跟服务器的连接：{e}");
        }


    }


    // 接受客户端发送的消息
    //public async void ReceiveMessage()
    //{
    //    try
    //    {
    //        var length = await Stream.ReadAsync(Message.Buffer, Message.Current, Message.BufferRemain);
    //        Console.WriteLine($"从网络流中读取到长度为{length}的消息！");
    //        // 读取数据，此操作是一个同步操作,但解析操作是异步的，因为同时操作缓冲区存在线程安全问题。
    //        Message.Read(length, ParseAsync);
    //        ReceiveMessage();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        Release();
    //    }
    //}

    public void SendMessage(byte[] message)
    {
        TcpClient.Client.Send(message);
    }

    //async Task ParseAsync(byte[] bytes) => await Task.Run(() => Server.Parse(bytes, this));


}

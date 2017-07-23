using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class UNETChat : Chat {

    private const short chatMessage = 1337;

    private void Start()
    {
        if (NetworkServer.active)
            NetworkServer.RegisterHandler(chatMessage, ServerReceiveMessage);

        NetworkManager.singleton.client.RegisterHandler(chatMessage, ReceiveMessage);
    }

    private void ReceiveMessage(NetworkMessage message)
    {
        //reading message
        string text = message.ReadMessage<StringMessage>().value;

        AddMessage(text);
    }

    public override void SendMessage(InputField input)
    {
        StringMessage myMessage = new StringMessage();

        if (input.text == "")
            return;

        //getting value of input
        myMessage.value = input.text;

        input.text = "";

        //sending to server
        NetworkManager.singleton.client.Send(chatMessage, myMessage);
    }

    private void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage myMessage = new StringMessage();
        int playerNum = message.conn.connectionId + 1;

        //we are using the connectionID as player name only to exemplify
        myMessage.value = "Player " + playerNum + ": " + message.ReadMessage<StringMessage>().value;

        //sending to all connected clients
        NetworkServer.SendToAll(chatMessage, myMessage);
    }
}

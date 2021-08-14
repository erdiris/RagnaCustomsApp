﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace RagnaCustoms.App.Views.Commandes
{
    class ShiftCommand: ICommandes
    {
        List<string> ICommandes.names()
        {
            return new List<string>() { "shift", "done" };
        }
        string ICommandes.help()
        {
            return "?";
        }
        bool ICommandes.action(
            JoinedChannel joinedChannel, 
            TextBox prefixe, 
            TwitchClient client, 
            TwitchBotForm me, 
            OnMessageReceivedArgs e
        )
        {
            if (e.ChatMessage.UserType == TwitchLib.Client.Enums.UserType.Viewer)
            {
                client.SendMessage(joinedChannel, $"Sorry only moderator can do that.");
            }
            else
            {
                try
                {
                    me.RemoveAtSongRequestInList(0);
                    client.SendMessage(joinedChannel, $"Song removed");

                    var sog = me.songRequests.Rows[0].Cells["Song"].Value.ToString();
                    if (sog != null)
                    {
                        client.SendMessage(joinedChannel, $"Next song : {sog} ");
                    }
                    else
                    {
                        client.SendMessage(joinedChannel, $"End of the queue");
                    }
                }
                catch (Exception O_o)
                {
                    client.SendMessage(joinedChannel, $"No More song to remove");
                }
            }
            return true;
        }
    }

}
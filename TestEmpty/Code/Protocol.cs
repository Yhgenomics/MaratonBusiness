﻿// Classes and structures being serialized

// Generated by ProtocolBuffer
// - a pure c# code generation implementation of protocol buffers
// Report bugs to: https://silentorbit.com/protobuf/

// DO NOT EDIT
// This file will be overwritten when CodeGenerator is run.
// To make custom modifications, edit the .proto file and add //:external before the message line
// then write the code and the changes in a separate file.
using System;
using System.Collections.Generic;

namespace Message
{
    public partial class MessageServantState
    {
        public int code { get; set; }

    }

    public partial class MessageServantStateReply
    {
        public string id { get; set; }

        public int state { get; set; }

        public int cpu { get; set; }

        public int memory { get; set; }

        public int type { get; set; }

    }

    public partial class MessageState
    {
        public int code { get; set; }

    }

    public partial class MessageStateReply
    {
        public int code { get; set; }

    }

    public partial class MessagePipe
    {
        public string id { get; set; }

        public string name { get; set; }

        public string executor { get; set; }

        public bool multipleInput { get; set; }

        public bool multipleThread { get; set; }

        public List<string> parameters { get; set; }

    }

    public partial class MessagePipeline
    {
        public string id { get; set; }

        public string name { get; set; }

        public List<Message.MessagePipe> pipes { get; set; }

    }

    public partial class MessageTaskDeliver
    {
        public string id { get; set; }

        public List<string> resources { get; set; }

        public List<string> input { get; set; }

        public List<string> servants { get; set; }

        public Message.MessagePipeline pipeline { get; set; }

        public bool isParallel { get; set; }

        public string originalID { get; set; }

    }

    public partial class MessageTaskDeliverReply
    {
        public string taskid { get; set; }

        public int code { get; set; }

        public string message { get; set; }

    }

}
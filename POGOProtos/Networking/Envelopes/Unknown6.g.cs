// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: POGOProtos/Networking/Envelopes/Unknown6.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Networking.Envelopes {

  /// <summary>Holder for reflection information generated from POGOProtos/Networking/Envelopes/Unknown6.proto</summary>
  public static partial class Unknown6Reflection {

    #region Descriptor
    /// <summary>File descriptor for POGOProtos/Networking/Envelopes/Unknown6.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static Unknown6Reflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ci5QT0dPUHJvdG9zL05ldHdvcmtpbmcvRW52ZWxvcGVzL1Vua25vd242LnBy",
            "b3RvEh9QT0dPUHJvdG9zLk5ldHdvcmtpbmcuRW52ZWxvcGVzIo8BCghVbmtu",
            "b3duNhIUCgxyZXF1ZXN0X3R5cGUYASABKAUSRAoIdW5rbm93bjIYAiABKAsy",
            "Mi5QT0dPUHJvdG9zLk5ldHdvcmtpbmcuRW52ZWxvcGVzLlVua25vd242LlVu",
            "a25vd24yGicKCFVua25vd24yEhsKE2VuY3J5cHRlZF9zaWduYXR1cmUYASAB",
            "KAxiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Envelopes.Unknown6), global::POGOProtos.Networking.Envelopes.Unknown6.Parser, new[]{ "RequestType", "Unknown2" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Envelopes.Unknown6.Types.Unknown2), global::POGOProtos.Networking.Envelopes.Unknown6.Types.Unknown2.Parser, new[]{ "EncryptedSignature" }, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Unknown6 : pb::IMessage<Unknown6> {
    private static readonly pb::MessageParser<Unknown6> _parser = new pb::MessageParser<Unknown6>(() => new Unknown6());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Unknown6> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::POGOProtos.Networking.Envelopes.Unknown6Reflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Unknown6() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Unknown6(Unknown6 other) : this() {
      requestType_ = other.requestType_;
      Unknown2 = other.unknown2_ != null ? other.Unknown2.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Unknown6 Clone() {
      return new Unknown6(this);
    }

    /// <summary>Field number for the "request_type" field.</summary>
    public const int RequestTypeFieldNumber = 1;
    private int requestType_;
    /// <summary>
    /// 5 for IAPs, 6 is unknown still
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int RequestType {
      get { return requestType_; }
      set {
        requestType_ = value;
      }
    }

    /// <summary>Field number for the "unknown2" field.</summary>
    public const int Unknown2FieldNumber = 2;
    private global::POGOProtos.Networking.Envelopes.Unknown6.Types.Unknown2 unknown2_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::POGOProtos.Networking.Envelopes.Unknown6.Types.Unknown2 Unknown2 {
      get { return unknown2_; }
      set {
        unknown2_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Unknown6);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Unknown6 other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (RequestType != other.RequestType) return false;
      if (!object.Equals(Unknown2, other.Unknown2)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (RequestType != 0) hash ^= RequestType.GetHashCode();
      if (unknown2_ != null) hash ^= Unknown2.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (RequestType != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(RequestType);
      }
      if (unknown2_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Unknown2);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (RequestType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RequestType);
      }
      if (unknown2_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Unknown2);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Unknown6 other) {
      if (other == null) {
        return;
      }
      if (other.RequestType != 0) {
        RequestType = other.RequestType;
      }
      if (other.unknown2_ != null) {
        if (unknown2_ == null) {
          unknown2_ = new global::POGOProtos.Networking.Envelopes.Unknown6.Types.Unknown2();
        }
        Unknown2.MergeFrom(other.Unknown2);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            RequestType = input.ReadInt32();
            break;
          }
          case 18: {
            if (unknown2_ == null) {
              unknown2_ = new global::POGOProtos.Networking.Envelopes.Unknown6.Types.Unknown2();
            }
            input.ReadMessage(unknown2_);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the Unknown6 message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class Unknown2 : pb::IMessage<Unknown2> {
        private static readonly pb::MessageParser<Unknown2> _parser = new pb::MessageParser<Unknown2>(() => new Unknown2());
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<Unknown2> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::POGOProtos.Networking.Envelopes.Unknown6.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Unknown2() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Unknown2(Unknown2 other) : this() {
          encryptedSignature_ = other.encryptedSignature_;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Unknown2 Clone() {
          return new Unknown2(this);
        }

        /// <summary>Field number for the "encrypted_signature" field.</summary>
        public const int EncryptedSignatureFieldNumber = 1;
        private pb::ByteString encryptedSignature_ = pb::ByteString.Empty;
        /// <summary>
        /// This are the bytes of POGOProtos.Networking/Envelopes/Signature.proto encrypted.
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pb::ByteString EncryptedSignature {
          get { return encryptedSignature_; }
          set {
            encryptedSignature_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as Unknown2);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(Unknown2 other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (EncryptedSignature != other.EncryptedSignature) return false;
          return true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (EncryptedSignature.Length != 0) hash ^= EncryptedSignature.GetHashCode();
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output) {
          if (EncryptedSignature.Length != 0) {
            output.WriteRawTag(10);
            output.WriteBytes(EncryptedSignature);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (EncryptedSignature.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeBytesSize(EncryptedSignature);
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(Unknown2 other) {
          if (other == null) {
            return;
          }
          if (other.EncryptedSignature.Length != 0) {
            EncryptedSignature = other.EncryptedSignature;
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                input.SkipLastField();
                break;
              case 10: {
                EncryptedSignature = input.ReadBytes();
                break;
              }
            }
          }
        }

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code

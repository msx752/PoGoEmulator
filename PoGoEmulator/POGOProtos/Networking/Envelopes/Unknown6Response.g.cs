// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: POGOProtos/Networking/Envelopes/Unknown6Response.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Networking.Envelopes {

  /// <summary>Holder for reflection information generated from POGOProtos/Networking/Envelopes/Unknown6Response.proto</summary>
  public static partial class Unknown6ResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for POGOProtos/Networking/Envelopes/Unknown6Response.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static Unknown6ResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjZQT0dPUHJvdG9zL05ldHdvcmtpbmcvRW52ZWxvcGVzL1Vua25vd242UmVz",
            "cG9uc2UucHJvdG8SH1BPR09Qcm90b3MuTmV0d29ya2luZy5FbnZlbG9wZXMa",
            "JVBPR09Qcm90b3MvRGF0YS9QbGF5ZXIvQ3VycmVuY3kucHJvdG8aKFBPR09Q",
            "cm90b3MvSW52ZW50b3J5L0l0ZW0vSXRlbURhdGEucHJvdG8ipgUKEFVua25v",
            "d242UmVzcG9uc2USFQoNcmVzcG9uc2VfdHlwZRgBIAEoBRJMCgh1bmtub3du",
            "MhgCIAEoCzI6LlBPR09Qcm90b3MuTmV0d29ya2luZy5FbnZlbG9wZXMuVW5r",
            "bm93bjZSZXNwb25zZS5Vbmtub3duMhqsBAoIVW5rbm93bjISEAoIdW5rbm93",
            "bjEYASABKAQSUwoFaXRlbXMYAiADKAsyRC5QT0dPUHJvdG9zLk5ldHdvcmtp",
            "bmcuRW52ZWxvcGVzLlVua25vd242UmVzcG9uc2UuVW5rbm93bjIuU3RvcmVJ",
            "dGVtEjsKEXBsYXllcl9jdXJyZW5jaWVzGAMgAygLMiAuUE9HT1Byb3Rvcy5E",
            "YXRhLlBsYXllci5DdXJyZW5jeRIQCgh1bmtub3duNBgEIAEoCRrpAgoJU3Rv",
            "cmVJdGVtEg8KB2l0ZW1faWQYASABKAkSDgoGaXNfaWFwGAIgASgIEjkKD2N1",
            "cnJlbmN5X3RvX2J1eRgDIAEoCzIgLlBPR09Qcm90b3MuRGF0YS5QbGF5ZXIu",
            "Q3VycmVuY3kSOQoPeWllbGRzX2N1cnJlbmN5GAQgASgLMiAuUE9HT1Byb3Rv",
            "cy5EYXRhLlBsYXllci5DdXJyZW5jeRI4Cgt5aWVsZHNfaXRlbRgFIAEoCzIj",
            "LlBPR09Qcm90b3MuSW52ZW50b3J5Lkl0ZW0uSXRlbURhdGESVgoEdGFncxgG",
            "IAMoCzJILlBPR09Qcm90b3MuTmV0d29ya2luZy5FbnZlbG9wZXMuVW5rbm93",
            "bjZSZXNwb25zZS5Vbmtub3duMi5TdG9yZUl0ZW0uVGFnEhAKCHVua25vd243",
            "GAcgASgFGiEKA1RhZxILCgNrZXkYASABKAkSDQoFdmFsdWUYAiABKAliBnBy",
            "b3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::POGOProtos.Data.Player.CurrencyReflection.Descriptor, global::POGOProtos.Inventory.Item.ItemDataReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Envelopes.Unknown6Response), global::POGOProtos.Networking.Envelopes.Unknown6Response.Parser, new[]{ "ResponseType", "Unknown2" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2), global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Parser, new[]{ "Unknown1", "Items", "PlayerCurrencies", "Unknown4" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem), global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Parser, new[]{ "ItemId", "IsIap", "CurrencyToBuy", "YieldsCurrency", "YieldsItem", "Tags", "Unknown7" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Types.Tag), global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Types.Tag.Parser, new[]{ "Key", "Value" }, null, null, null)})})})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Unknown6Response : pb::IMessage<Unknown6Response> {
    private static readonly pb::MessageParser<Unknown6Response> _parser = new pb::MessageParser<Unknown6Response>(() => new Unknown6Response());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Unknown6Response> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::POGOProtos.Networking.Envelopes.Unknown6ResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Unknown6Response() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Unknown6Response(Unknown6Response other) : this() {
      responseType_ = other.responseType_;
      Unknown2 = other.unknown2_ != null ? other.Unknown2.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Unknown6Response Clone() {
      return new Unknown6Response(this);
    }

    /// <summary>Field number for the "response_type" field.</summary>
    public const int ResponseTypeFieldNumber = 1;
    private int responseType_;
    /// <summary>
    /// Still don't know what 6 is, but 5 lists items available via IAPs.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ResponseType {
      get { return responseType_; }
      set {
        responseType_ = value;
      }
    }

    /// <summary>Field number for the "unknown2" field.</summary>
    public const int Unknown2FieldNumber = 2;
    private global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2 unknown2_;
    /// <summary>
    /// Response data
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2 Unknown2 {
      get { return unknown2_; }
      set {
        unknown2_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Unknown6Response);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Unknown6Response other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ResponseType != other.ResponseType) return false;
      if (!object.Equals(Unknown2, other.Unknown2)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ResponseType != 0) hash ^= ResponseType.GetHashCode();
      if (unknown2_ != null) hash ^= Unknown2.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ResponseType != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ResponseType);
      }
      if (unknown2_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Unknown2);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ResponseType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ResponseType);
      }
      if (unknown2_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Unknown2);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Unknown6Response other) {
      if (other == null) {
        return;
      }
      if (other.ResponseType != 0) {
        ResponseType = other.ResponseType;
      }
      if (other.unknown2_ != null) {
        if (unknown2_ == null) {
          unknown2_ = new global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2();
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
            ResponseType = input.ReadInt32();
            break;
          }
          case 18: {
            if (unknown2_ == null) {
              unknown2_ = new global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2();
            }
            input.ReadMessage(unknown2_);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the Unknown6Response message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class Unknown2 : pb::IMessage<Unknown2> {
        private static readonly pb::MessageParser<Unknown2> _parser = new pb::MessageParser<Unknown2>(() => new Unknown2());
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<Unknown2> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::POGOProtos.Networking.Envelopes.Unknown6Response.Descriptor.NestedTypes[0]; }
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
          unknown1_ = other.unknown1_;
          items_ = other.items_.Clone();
          playerCurrencies_ = other.playerCurrencies_.Clone();
          unknown4_ = other.unknown4_;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Unknown2 Clone() {
          return new Unknown2(this);
        }

        /// <summary>Field number for the "unknown1" field.</summary>
        public const int Unknown1FieldNumber = 1;
        private ulong unknown1_;
        /// <summary>
        /// Maybe status? It's always 1 (success), so it's probably that.
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public ulong Unknown1 {
          get { return unknown1_; }
          set {
            unknown1_ = value;
          }
        }

        /// <summary>Field number for the "items" field.</summary>
        public const int ItemsFieldNumber = 2;
        private static readonly pb::FieldCodec<global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem> _repeated_items_codec
            = pb::FieldCodec.ForMessage(18, global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Parser);
        private readonly pbc::RepeatedField<global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem> items_ = new pbc::RepeatedField<global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem>();
        /// <summary>
        /// Items to show in the shop
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pbc::RepeatedField<global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem> Items {
          get { return items_; }
        }

        /// <summary>Field number for the "player_currencies" field.</summary>
        public const int PlayerCurrenciesFieldNumber = 3;
        private static readonly pb::FieldCodec<global::POGOProtos.Data.Player.Currency> _repeated_playerCurrencies_codec
            = pb::FieldCodec.ForMessage(26, global::POGOProtos.Data.Player.Currency.Parser);
        private readonly pbc::RepeatedField<global::POGOProtos.Data.Player.Currency> playerCurrencies_ = new pbc::RepeatedField<global::POGOProtos.Data.Player.Currency>();
        /// <summary>
        /// currencies that player has at the moment
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pbc::RepeatedField<global::POGOProtos.Data.Player.Currency> PlayerCurrencies {
          get { return playerCurrencies_; }
        }

        /// <summary>Field number for the "unknown4" field.</summary>
        public const int Unknown4FieldNumber = 4;
        private string unknown4_ = "";
        /// <summary>
        /// Some base64 encoded stuff...
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string Unknown4 {
          get { return unknown4_; }
          set {
            unknown4_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
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
          if (Unknown1 != other.Unknown1) return false;
          if(!items_.Equals(other.items_)) return false;
          if(!playerCurrencies_.Equals(other.playerCurrencies_)) return false;
          if (Unknown4 != other.Unknown4) return false;
          return true;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (Unknown1 != 0UL) hash ^= Unknown1.GetHashCode();
          hash ^= items_.GetHashCode();
          hash ^= playerCurrencies_.GetHashCode();
          if (Unknown4.Length != 0) hash ^= Unknown4.GetHashCode();
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output) {
          if (Unknown1 != 0UL) {
            output.WriteRawTag(8);
            output.WriteUInt64(Unknown1);
          }
          items_.WriteTo(output, _repeated_items_codec);
          playerCurrencies_.WriteTo(output, _repeated_playerCurrencies_codec);
          if (Unknown4.Length != 0) {
            output.WriteRawTag(34);
            output.WriteString(Unknown4);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (Unknown1 != 0UL) {
            size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Unknown1);
          }
          size += items_.CalculateSize(_repeated_items_codec);
          size += playerCurrencies_.CalculateSize(_repeated_playerCurrencies_codec);
          if (Unknown4.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Unknown4);
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(Unknown2 other) {
          if (other == null) {
            return;
          }
          if (other.Unknown1 != 0UL) {
            Unknown1 = other.Unknown1;
          }
          items_.Add(other.items_);
          playerCurrencies_.Add(other.playerCurrencies_);
          if (other.Unknown4.Length != 0) {
            Unknown4 = other.Unknown4;
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
                Unknown1 = input.ReadUInt64();
                break;
              }
              case 18: {
                items_.AddEntriesFrom(input, _repeated_items_codec);
                break;
              }
              case 26: {
                playerCurrencies_.AddEntriesFrom(input, _repeated_playerCurrencies_codec);
                break;
              }
              case 34: {
                Unknown4 = input.ReadString();
                break;
              }
            }
          }
        }

        #region Nested types
        /// <summary>Container for nested types declared in the Unknown2 message type.</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static partial class Types {
          public sealed partial class StoreItem : pb::IMessage<StoreItem> {
            private static readonly pb::MessageParser<StoreItem> _parser = new pb::MessageParser<StoreItem>(() => new StoreItem());
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public static pb::MessageParser<StoreItem> Parser { get { return _parser; } }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public static pbr::MessageDescriptor Descriptor {
              get { return global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Descriptor.NestedTypes[0]; }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            pbr::MessageDescriptor pb::IMessage.Descriptor {
              get { return Descriptor; }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public StoreItem() {
              OnConstruction();
            }

            partial void OnConstruction();

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public StoreItem(StoreItem other) : this() {
              itemId_ = other.itemId_;
              isIap_ = other.isIap_;
              CurrencyToBuy = other.currencyToBuy_ != null ? other.CurrencyToBuy.Clone() : null;
              YieldsCurrency = other.yieldsCurrency_ != null ? other.YieldsCurrency.Clone() : null;
              YieldsItem = other.yieldsItem_ != null ? other.YieldsItem.Clone() : null;
              tags_ = other.tags_.Clone();
              unknown7_ = other.unknown7_;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public StoreItem Clone() {
              return new StoreItem(this);
            }

            /// <summary>Field number for the "item_id" field.</summary>
            public const int ItemIdFieldNumber = 1;
            private string itemId_ = "";
            /// <summary>
            /// Internal ID (probably for Google Play/App Store) example: "pgorelease.incenseordinary.1"
            /// </summary>
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public string ItemId {
              get { return itemId_; }
              set {
                itemId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
              }
            }

            /// <summary>Field number for the "is_iap" field.</summary>
            public const int IsIapFieldNumber = 2;
            private bool isIap_;
            /// <summary>
            /// If true, this item is bought with real currency (USD, etc.) through the Play/App Store instead of Pokecoins
            /// </summary>
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public bool IsIap {
              get { return isIap_; }
              set {
                isIap_ = value;
              }
            }

            /// <summary>Field number for the "currency_to_buy" field.</summary>
            public const int CurrencyToBuyFieldNumber = 3;
            private global::POGOProtos.Data.Player.Currency currencyToBuy_;
            /// <summary>
            /// This defines how much the item costs (with the exception of items that cost real money like Pokecoins, that's defined in the respective store)
            /// </summary>
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public global::POGOProtos.Data.Player.Currency CurrencyToBuy {
              get { return currencyToBuy_; }
              set {
                currencyToBuy_ = value;
              }
            }

            /// <summary>Field number for the "yields_currency" field.</summary>
            public const int YieldsCurrencyFieldNumber = 4;
            private global::POGOProtos.Data.Player.Currency yieldsCurrency_;
            /// <summary>
            /// When bought, this IAP will yield this much currency
            /// </summary>
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public global::POGOProtos.Data.Player.Currency YieldsCurrency {
              get { return yieldsCurrency_; }
              set {
                yieldsCurrency_ = value;
              }
            }

            /// <summary>Field number for the "yields_item" field.</summary>
            public const int YieldsItemFieldNumber = 5;
            private global::POGOProtos.Inventory.Item.ItemData yieldsItem_;
            /// <summary>
            /// The item and count of such item that this IAP will yield
            /// </summary>
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public global::POGOProtos.Inventory.Item.ItemData YieldsItem {
              get { return yieldsItem_; }
              set {
                yieldsItem_ = value;
              }
            }

            /// <summary>Field number for the "tags" field.</summary>
            public const int TagsFieldNumber = 6;
            private static readonly pb::FieldCodec<global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Types.Tag> _repeated_tags_codec
                = pb::FieldCodec.ForMessage(50, global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Types.Tag.Parser);
            private readonly pbc::RepeatedField<global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Types.Tag> tags_ = new pbc::RepeatedField<global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Types.Tag>();
            /// <summary>
            /// Stuff like SORT:12, CATEGORY:ITEMS
            /// </summary>
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public pbc::RepeatedField<global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Types.Tag> Tags {
              get { return tags_; }
            }

            /// <summary>Field number for the "unknown7" field.</summary>
            public const int Unknown7FieldNumber = 7;
            private int unknown7_;
            /// <summary>
            /// Possibly something to toggle visibility in the store/purchasibility?
            /// </summary>
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public int Unknown7 {
              get { return unknown7_; }
              set {
                unknown7_ = value;
              }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public override bool Equals(object other) {
              return Equals(other as StoreItem);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public bool Equals(StoreItem other) {
              if (ReferenceEquals(other, null)) {
                return false;
              }
              if (ReferenceEquals(other, this)) {
                return true;
              }
              if (ItemId != other.ItemId) return false;
              if (IsIap != other.IsIap) return false;
              if (!object.Equals(CurrencyToBuy, other.CurrencyToBuy)) return false;
              if (!object.Equals(YieldsCurrency, other.YieldsCurrency)) return false;
              if (!object.Equals(YieldsItem, other.YieldsItem)) return false;
              if(!tags_.Equals(other.tags_)) return false;
              if (Unknown7 != other.Unknown7) return false;
              return true;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public override int GetHashCode() {
              int hash = 1;
              if (ItemId.Length != 0) hash ^= ItemId.GetHashCode();
              if (IsIap != false) hash ^= IsIap.GetHashCode();
              if (currencyToBuy_ != null) hash ^= CurrencyToBuy.GetHashCode();
              if (yieldsCurrency_ != null) hash ^= YieldsCurrency.GetHashCode();
              if (yieldsItem_ != null) hash ^= YieldsItem.GetHashCode();
              hash ^= tags_.GetHashCode();
              if (Unknown7 != 0) hash ^= Unknown7.GetHashCode();
              return hash;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public override string ToString() {
              return pb::JsonFormatter.ToDiagnosticString(this);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public void WriteTo(pb::CodedOutputStream output) {
              if (ItemId.Length != 0) {
                output.WriteRawTag(10);
                output.WriteString(ItemId);
              }
              if (IsIap != false) {
                output.WriteRawTag(16);
                output.WriteBool(IsIap);
              }
              if (currencyToBuy_ != null) {
                output.WriteRawTag(26);
                output.WriteMessage(CurrencyToBuy);
              }
              if (yieldsCurrency_ != null) {
                output.WriteRawTag(34);
                output.WriteMessage(YieldsCurrency);
              }
              if (yieldsItem_ != null) {
                output.WriteRawTag(42);
                output.WriteMessage(YieldsItem);
              }
              tags_.WriteTo(output, _repeated_tags_codec);
              if (Unknown7 != 0) {
                output.WriteRawTag(56);
                output.WriteInt32(Unknown7);
              }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public int CalculateSize() {
              int size = 0;
              if (ItemId.Length != 0) {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(ItemId);
              }
              if (IsIap != false) {
                size += 1 + 1;
              }
              if (currencyToBuy_ != null) {
                size += 1 + pb::CodedOutputStream.ComputeMessageSize(CurrencyToBuy);
              }
              if (yieldsCurrency_ != null) {
                size += 1 + pb::CodedOutputStream.ComputeMessageSize(YieldsCurrency);
              }
              if (yieldsItem_ != null) {
                size += 1 + pb::CodedOutputStream.ComputeMessageSize(YieldsItem);
              }
              size += tags_.CalculateSize(_repeated_tags_codec);
              if (Unknown7 != 0) {
                size += 1 + pb::CodedOutputStream.ComputeInt32Size(Unknown7);
              }
              return size;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public void MergeFrom(StoreItem other) {
              if (other == null) {
                return;
              }
              if (other.ItemId.Length != 0) {
                ItemId = other.ItemId;
              }
              if (other.IsIap != false) {
                IsIap = other.IsIap;
              }
              if (other.currencyToBuy_ != null) {
                if (currencyToBuy_ == null) {
                  currencyToBuy_ = new global::POGOProtos.Data.Player.Currency();
                }
                CurrencyToBuy.MergeFrom(other.CurrencyToBuy);
              }
              if (other.yieldsCurrency_ != null) {
                if (yieldsCurrency_ == null) {
                  yieldsCurrency_ = new global::POGOProtos.Data.Player.Currency();
                }
                YieldsCurrency.MergeFrom(other.YieldsCurrency);
              }
              if (other.yieldsItem_ != null) {
                if (yieldsItem_ == null) {
                  yieldsItem_ = new global::POGOProtos.Inventory.Item.ItemData();
                }
                YieldsItem.MergeFrom(other.YieldsItem);
              }
              tags_.Add(other.tags_);
              if (other.Unknown7 != 0) {
                Unknown7 = other.Unknown7;
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
                    ItemId = input.ReadString();
                    break;
                  }
                  case 16: {
                    IsIap = input.ReadBool();
                    break;
                  }
                  case 26: {
                    if (currencyToBuy_ == null) {
                      currencyToBuy_ = new global::POGOProtos.Data.Player.Currency();
                    }
                    input.ReadMessage(currencyToBuy_);
                    break;
                  }
                  case 34: {
                    if (yieldsCurrency_ == null) {
                      yieldsCurrency_ = new global::POGOProtos.Data.Player.Currency();
                    }
                    input.ReadMessage(yieldsCurrency_);
                    break;
                  }
                  case 42: {
                    if (yieldsItem_ == null) {
                      yieldsItem_ = new global::POGOProtos.Inventory.Item.ItemData();
                    }
                    input.ReadMessage(yieldsItem_);
                    break;
                  }
                  case 50: {
                    tags_.AddEntriesFrom(input, _repeated_tags_codec);
                    break;
                  }
                  case 56: {
                    Unknown7 = input.ReadInt32();
                    break;
                  }
                }
              }
            }

            #region Nested types
            /// <summary>Container for nested types declared in the StoreItem message type.</summary>
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public static partial class Types {
              public sealed partial class Tag : pb::IMessage<Tag> {
                private static readonly pb::MessageParser<Tag> _parser = new pb::MessageParser<Tag>(() => new Tag());
                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public static pb::MessageParser<Tag> Parser { get { return _parser; } }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public static pbr::MessageDescriptor Descriptor {
                  get { return global::POGOProtos.Networking.Envelopes.Unknown6Response.Types.Unknown2.Types.StoreItem.Descriptor.NestedTypes[0]; }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                pbr::MessageDescriptor pb::IMessage.Descriptor {
                  get { return Descriptor; }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public Tag() {
                  OnConstruction();
                }

                partial void OnConstruction();

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public Tag(Tag other) : this() {
                  key_ = other.key_;
                  value_ = other.value_;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public Tag Clone() {
                  return new Tag(this);
                }

                /// <summary>Field number for the "key" field.</summary>
                public const int KeyFieldNumber = 1;
                private string key_ = "";
                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public string Key {
                  get { return key_; }
                  set {
                    key_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
                  }
                }

                /// <summary>Field number for the "value" field.</summary>
                public const int ValueFieldNumber = 2;
                private string value_ = "";
                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public string Value {
                  get { return value_; }
                  set {
                    value_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
                  }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public override bool Equals(object other) {
                  return Equals(other as Tag);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public bool Equals(Tag other) {
                  if (ReferenceEquals(other, null)) {
                    return false;
                  }
                  if (ReferenceEquals(other, this)) {
                    return true;
                  }
                  if (Key != other.Key) return false;
                  if (Value != other.Value) return false;
                  return true;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public override int GetHashCode() {
                  int hash = 1;
                  if (Key.Length != 0) hash ^= Key.GetHashCode();
                  if (Value.Length != 0) hash ^= Value.GetHashCode();
                  return hash;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public override string ToString() {
                  return pb::JsonFormatter.ToDiagnosticString(this);
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public void WriteTo(pb::CodedOutputStream output) {
                  if (Key.Length != 0) {
                    output.WriteRawTag(10);
                    output.WriteString(Key);
                  }
                  if (Value.Length != 0) {
                    output.WriteRawTag(18);
                    output.WriteString(Value);
                  }
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public int CalculateSize() {
                  int size = 0;
                  if (Key.Length != 0) {
                    size += 1 + pb::CodedOutputStream.ComputeStringSize(Key);
                  }
                  if (Value.Length != 0) {
                    size += 1 + pb::CodedOutputStream.ComputeStringSize(Value);
                  }
                  return size;
                }

                [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
                public void MergeFrom(Tag other) {
                  if (other == null) {
                    return;
                  }
                  if (other.Key.Length != 0) {
                    Key = other.Key;
                  }
                  if (other.Value.Length != 0) {
                    Value = other.Value;
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
                        Key = input.ReadString();
                        break;
                      }
                      case 18: {
                        Value = input.ReadString();
                        break;
                      }
                    }
                  }
                }

              }

            }
            #endregion

          }

        }
        #endregion

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code

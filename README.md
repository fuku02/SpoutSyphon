# SpoutSyphon

### UnityEditor + UnityApp + TouchDesigner on Mac<br>
[![UnitySpoutSyphon_Mac](https://img.youtube.com/vi/bNbexI9FOLA/0.jpg)](https://www.youtube.com/watch?v=bNbexI9FOLA)
<br>
### UnityEditor + UnityApp + TouchDesigner on Win<br>
[![UnitySpoutSyphon_Win](https://img.youtube.com/vi/42qos55oARk/0.jpg)](https://www.youtube.com/watch?v=42qos55oARk)

KlakSpout+KlakSyphonを組み合わせてWin/Mac両方で使えるようにしたUnityパッケージです。
起動時にプラットフォームを判定して、KlakSpout と KlakSyphon を切り替えます。
KlakSpout と KlakSyphon の良いとこ取りをして、プラットフォームを気にせず使えるようになっています。

# Features
## Sender
- 任意のSenderNameをつけられる
- SpoutでもSyphon形式のSenderNameに整形（AppName:SenderName）
- 送信動画の解像度を指定可能

## Receiver
- AutoConnect機能（任意のSenderNameのSenderと自動接続）
- Sender一覧 + セレクター機能
- 受信動画の解像度を指定可能

# 環境
- OS X Catalina(10.15.7)
- Windows 10
- Unity : 2020.3.7f1
- KlakSpout v0.2.4
- KlakSyphon v0.0.3

# Install
KlakSpout.unitypackage と KlakSyphon.unitypackageをダウンロードし、Importしてください。<br>
KlakSpoutは scoped registry にも対応していますが、安定バージョンで固定するために、packageからのImportをお勧めします。

### unity : package
- KlakSpout
 https://github.com/keijiro/KlakSpout
- KlakSyphon
 https://github.com/keijiro/KlakSyphon

# Usage
## Sender として使う
![sender_settings_2](https://user-images.githubusercontent.com/57790558/118239675-0cc1e700-b4d5-11eb-84f7-4475eed4f858.png)

SpoutSyphonSender を Main Camera にアタッチし、SenderNameを設定します。<br>
再生時には、KlakSpout もしくは KlakSyphon のコンポーネントが追加され、Main Camera の名前が SenderNameに置き換わります。<br>


![sender_settings_1](https://user-images.githubusercontent.com/57790558/118227752-0b3bf300-b4c4-11eb-86f8-9793dd139a18.png)

送信解像度を指定するには、SetResolution を 任意のGameObject にアタッチします。(ここでは Main に付けています。)<br>
UnityEditor上では、GameViewの解像度を変更することで、送信解像度を変更できます。<br>
他にも SetFrameRate や SetQuality を用意していますが、必須ではありません。<br>

## Receiver として使う
![recaiver_settings](https://user-images.githubusercontent.com/57790558/118239698-14818b80-b4d5-11eb-86a0-61fb24cc5f16.png)

SpoutSyphonReceiver を RawImage にアタッチします。<br>
AutoConnect を True にし、SenderName を設定すると、指定した SenderName の Sender と自動接続します。<br>
RawImage用の RenderTextrue は再生時に指定解像度で自動作成されます。<br>

## DebugMode について
<img width="799" alt="スクリーンショット 2021-05-14 17 01 43" src="https://user-images.githubusercontent.com/57790558/118240583-231c7280-b4d6-11eb-95f6-64d1e5d0c4f6.png">

キーボードの Dキー を押すと DebugMode になります。<br>
DebugMode では、fps が表示され、Receiver の場合は、Sendderセレクターが表示されます。


# 参考
- 送信動画の解像度の指定方法の参考にしました<br>
 https://github.com/SJ-magic-youtube-VisualProgrammer/6__syphon_unity_to_oF

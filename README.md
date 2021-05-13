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
- 送信動画の解像度が指定可能

## Receiver
- AutoConnect機能（任意のSenderNameのSenderと自動接続）
- Sender一覧 + セレクター機能
- 受信動画の解像度が指定可能

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
## Sender の場合
SpoutSyphonSender を Main Camera にアタッチします。
解像度を指定するには、SetResolution を 任意のGameObject にアタッチします。(GameObjectならどれでも問題ない)

## Receiver の場合
SpoutSyphonReceiver をRawImageにアタッチします。
RawImage用のRenderTextrueは再生時に自動作成されます。

キーボードの Dキー を押すとデバッグモードになります。
デバッグモードでは、fps が表示され、Receiver の場合は、Sendderセレクターが表示されます。

詳細は後日

# 参考
- 送信動画の解像度の指定方法の参考にしました<br>
 https://github.com/SJ-magic-youtube-VisualProgrammer/6__syphon_unity_to_oF

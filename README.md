
<!-- TOC -->

- [MillionHoursTimer](#millionhourstimer)
- [実行環境](#実行環境)
- [はじめに](#はじめに)
    - [インストールと起動方法](#インストールと起動方法)
        - [インストーラー版](#インストーラー版)
        - [バイナリ版](#バイナリ版)
    - [終了方法](#終了方法)
- [使い方](#使い方)
    - [起動時間の記録](#起動時間の記録)
    - [アプリの登録](#アプリの登録)
    - [ファイル別作業時間の記録](#ファイル別作業時間の記録)
- [重要：Clip Studio Paintに関して](#重要clip-studio-paintに関して)
- [Togglとの連携](#togglとの連携)
    - [連携手順](#連携手順)
- [注意事項](#注意事項)
- [連絡先](#連絡先)

<!-- /TOC -->

# MillionHoursTimer
MillionHoursTimerは、指定したアプリケーションの起動時間を自動で記録するためのWindows向け常駐型アプリです。開いているファイル名毎に作業時間を記録することや、Togglとの連携によるWeb上でのサマリーレポートの確認が可能です。日々の時間管理やモチベーションの維持等にお役立てください。
![image](./demo.png?raw=true "demo")
![image](./demo2.png?raw=true "demo2")

# 実行環境
Windows 8/10  
.NET Framework 4.6

# はじめに
## インストールと起動方法
こちらからダウンロードしてください。インストーラー版とインストール不要で使えるバイナリ版の2つがあります。どちらをお使いいただいても構いません。
### インストーラー版
ダウンロード後、Setup.exeよりインストールしてください。``C:\Program Files (x86)``下にファイルが配置されます。インストール後、スタートメニューとデスクトップにショートカットが作成されるので、そちらから起動してください。アンインストールする際は同様にSetup.exeより行っていただくか、コントロールパネル>プログラムのアンインストールより行ってください。
### バイナリ版
ダウンロード後、フォルダを適当な場所に配置したあと、MHTimer.exeから起動してください。削除するときはフォルダごとそのまま削除してください。

## 終了方法
本アプリは最小化するか☓ボタンを押すと、タスクトレイに常駐されます。
<u>終了するときはタスクトレイのアイコンを右クリック>終了　をクリックしてください。</u>
なお起動後に設定>システム起動時に自動的に起動　のチェックを入れることで、PC起動時に自動で起動することができます。

# 使い方
## 起動時間の記録
画面内に表示されているアプリが起動しているとき、起動時間が自動で記録されます。記録中のアプリは背景が赤色で表示されます。
起動時間の計測方法には3種類あり、設定から自由に変更できます。

* 起動しているアプリを全て計測  
バックグラウンドで動作しているものも含め、起動している全てのアプリを計測対象に含めます。設定>記録設定のうち、以下の2つの項目にチェックを入れない場合この設定が適用されます。

* 最小化していないアプリのみ計測する  
デフォルトの設定です。設定>記録設定>最小化しているアプリケーションを計測しない　にチェックを入れた場合に適用されます。この設定では、最小化しているアプリを計測対象に含めません。

* アクティブウィンドウのみを計測する  
設定>記録設定>アクティブウィンドウのみを計測する　にチェックを入れた場合に適用されます。
現在アクティブになっているウィンドウ（作業中のウィンドウ）のアプリのみ計測対象となります。

## アプリの登録
起動時間を記録するアプリを自由に登録することができます。記録したいアプリケーションの実行ファイル(.exe)、またはショートカットファイル(.lnk)を画面内にドラッグ＆ドロップしてください。またはファイル設定>アプリケーションの登録より、直接実行ファイルのパスを入力してください。
<!-- ![image](./registration.png?raw=true "clip_cmc") -->

## ファイル別作業時間の記録
タイトルバーにファイル名+拡張子が表示されるアプリケーションに関しては、ファイル拡張子を設定することで、作業しているファイル別に作業時間を計測することが可能です。
リストからアプリ名を右クリック>ファイル拡張子の設定　をクリック後、
表示されるウィンドウにアプリケーションに応じた拡張子を半角スラッシュ区切りで設定してください。  
例)イラスト系ソフトの場合  
`.gif/.jpg/.jpeg/.png/.bmp/.ico/.tif/.tiff/.tga/.psd/.psb/.sai`  

現在のところ、上記の条件でファイル別作業時間の記録が可能なアプリケーションには以下のものを確認しています。<b>なおPhotoshopの場合、一度名前を付けてファイルを保存してからでないとファイル名が表示されませんので注意してください。</b>
* イラスト、画像編集系ソフト  
Photoshop(CC 2018)、PaintTool SAI、Gimp
* エディタ系ソフト  
VS Code、Atom、Microsoft Word、IntelliJ、サクラエディタ、Terapad、TexStudio

それ以外でも、**タイトルバーにファイル名+ハイフン(-)が表示されるアプリケーション(Wordなど)**
に関しては、設定>ファイル設定>ハイフン区切りをファイル名の取得に利用する　を有効にした場合にファイル名が記録されます。

なお、ファイル名でデータを区別しているため、途中でファイル名を変更すると別のデータとして保存されることに注意してください。

# 重要：Clip Studio Paintに関して
Clip Studio Paintでは仕様上、メインウィンドウのタイトルバーにファイル名が表示されないため、そのままではファイル名の取得ができません。この場合、下図に示すように<u><b>「ページ管理ウィンドウ」もしくは「ストーリーエディター」のいずれかを、Clip Studio Paintのメインウィンドウからフローティングし別ウィンドウで表示する</b></u>ことで、記録が可能になります。この場合、設定>ファイル設定>ファイル名をスペースで区切る　を有効にすることをお勧めします（デフォルトで有効になっています）。

<u>なお、「最小化していないアプリのみ計測する」または「アクティブウィンドウのみを計測する」設定を有効にしている場合で、フローティングしたウィンドウを最小化すると、ファイル名が記録されなくなります。</u>フローティングしたウィンドウを最小化しておきたい場合は、設定>ファイル設定>ファイル名表示ウィンドウの計測条件を無視　を有効にしてください。

![image](./clipstudioSetting.png?raw=true "clip_window")

現時点では、記録できるのは作品ごとの作業時間に限られます。<u>複数ページファイルのうち
特定のページのみ記録する方法に関しては、現在のところありません。</u>もし要望があれば今後対応する可能性はあります。

# Togglとの連携
<!-- 記録は、インストーラー版なら``C:\Program Files (x86)\MHTimer\data``フォルダに、バイナリ版なら
``MHTimer\data``フォルダに保存されますが、 -->
<u>継続して記録をつける場合、時間管理用Webサービス、[Toggl](https://toggl.com)との連携を強くお勧めします。</u>

## 連携手順
アカウントがない場合は登録していただき、上記URLよりブラウザからTogglアカウントにログイン後、以下の手順で連携を行ってください。

1. メニュー下部より「Profile setting」を開く
2. 画面最下部に表示されている「API token」について、右側のResetボタンを押す(省略可能です。認証が失敗する場合にお試しください)
3. 表示されているキーをコピー
4. 設定>Toggl連携設定　のAPI キー欄にコピーした内容を貼り付け後、OKボタンを押す

4.を実行後、「認証が完了しました」と表示され、登録したメールアドレスが表示されれば連携完了です。

![image](./togglSetting.png?raw=true "toggl_setting")

連携後、記録しているアプリが終了されたとき、Togglアカウントににアプリの実行時間が送信されます。
実際にToggl上で時間が記録されていることをご確認ください。

![image](./timeEntry.png?raw=true "toggl_display")

各アプリにはそれぞれ、「プロジェクト」と「タグ」を設定することができます。設定>Toggl連携設定　の下部リストより設定してください。その他Togglの使い方については[公式ドキュメント](https://support.toggl.com/en/)をご覧ください。

# 注意事項
- <b>当アプリで発生したいかなる不具合に関しても、開発者は一切の責任を負いません。あくまでも使用は自己責任でお願いいたします。</b>
- 管理者権限でアプリを実行した場合、ドラッグアンドドロップによるアプリの追加は行なえません。
- MHTimer.exeをクリックしても起動できない場合、一度管理者権限で実行してから終了後、再度実行してみてください。それでも実行ができない場合、申し訳ありませんが開発者までご連絡ください。
- 記録するアプリによってはうまく記録が行えない場合があります。現在確認している例として以下のものが存在します。
  - 「Kindle for PC」を起動時、「最小化していないアプリのみ計測する」を有効にしても、最小化時に記録が継続されることがある
- バイナリ版をご利用の場合、アプリを削除する際には「システム起動時に自動的に起動」を無効にしてから削除することをお勧めします。

# 連絡先
バグ報告やご意見を頂ける場合は、Issuesか作者Twitter(@kanchi0914)までご連絡ください。
# ゲームのタイトルを入れてください

## GameJam-Template
GameJamを想定したUnity用テンプレート

## プレイ動画
> URLを挿入

## クレジット
### お名前
- 職種 プログラマー
  - 担当箇所 xxx
  - 所属 xxx

## 開発環境
| エンジン | バージョン  |
| ---------- | ----------- |
| Unity      | [こちらを参照して下さい](ProjectSettings/ProjectVersion.txt#L1) |

## 導入済みアセット
> **[DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676?locale=ja-JP)**
> **[UniRx](https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts)**
> **[UniTask](https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask)**

## 素材
>　URLを挿入

## 命名規則
### C#
|  | 命名方法 | 例 |
| - | - | - |
| 名前空間 | [パスカルケース](https://wa3.i-3-i.info/word13955.html) | NameSpace |
| クラス | [パスカルケース](https://wa3.i-3-i.info/word13955.html) | ClassName |
| プロパティ | [パスカルケース](https://wa3.i-3-i.info/word13955.html) | PropertyName |
| 変数 | [ローワーキャメルケース](https://e-words.jp/w/%E3%82%AD%E3%83%A3%E3%83%A1%E3%83%AB%E3%82%B1%E3%83%BC%E3%82%B9.html) | variableName |
| メンバー変数 | 「_」 + 変数名 | _●● |
| bool型変数 | 「is」 + 変数名 | is●● |
| UI型変数 | 変数名 + UI名 | ●●Text |
| 定数 | [アッパースネークケース](https://e-words.jp/w/%E3%82%B9%E3%83%8D%E3%83%BC%E3%82%AF%E3%82%B1%E3%83%BC%E3%82%B9.html#:~:text=%E3%82%B9%E3%83%8D%E3%83%BC%E3%82%AF%E3%82%B1%E3%83%BC%E3%82%B9%E3%81%A8%E3%81%AF%E3%80%81%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0,%E3%81%AA%E8%A1%A8%E8%A8%98%E3%81%8C%E3%81%93%E3%82%8C%E3%81%AB%E5%BD%93%E3%81%9F%E3%82%8B%E3%80%82) | VARIABLE_NAME |
| イベント | [パスカルケース](https://wa3.i-3-i.info/word13955.html) & 「On」 + イベント名 | On●● |
| 関数 | [パスカルケース](https://wa3.i-3-i.info/word13955.html) | MethodName |

### Unity
|  | 命名方法 | 例 |
| - | - | - |
| アセット | [パスカルケース](https://wa3.i-3-i.info/word13955.html) | AssetName |
| ファイル | [パスカルケース](https://wa3.i-3-i.info/word13955.html) | FileName |
| オブジェクト | [パスカルケース](https://wa3.i-3-i.info/word13955.html) | ObjectName |
| UIオブジェクト | オブジェクト名 + UI名 | ●●Text |

### Sourcetree
|  | 命名方法 | 例 |
| - | - | - |
| ブランチ | スネークケース | branch_name |
| 機能作成 | 「feature/」 + ブランチ名 | feature/branch_name |
| 機能修正 | 「fix/」 + ブランチ名 | fix/branch_name |
| 機能削除 | 「remove/」 + ブランチ名 | remove/branch_name |

## region 規則
```shell
public class AnyName
{
    #region Properties
        // プロパティ
    #region Inspector Variables
        // Unity Inspectorに表示したいもの
    #region Member Variables
        // プライベートなメンバー変数
    #region Constants
        // 定数
    #region Events
        // System.Action, System.Func などのデリゲートやコールバック関数
    #region Constructor
        // コンストラクタ
    #region Unity Methods
        // Start, UpdateなどのUnityのイベント関数
    #region Enums
        // 列挙型
    #region Public Methods
        // 自作のPublicな関数
    #region Private Methods
        // 自作のPrivateな関数
}
```

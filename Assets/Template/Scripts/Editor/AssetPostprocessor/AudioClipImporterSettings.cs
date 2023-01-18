using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// オーディオクリップをインポートした時にの設定を変更する
/// https://amagamina.jp/blog/audio-clip/
/// https://qiita.com/ptkyoku/items/84b62cfbb4282a7cd7e6
/// https://kan-kikuchi.hatenablog.com/entry/AudioSettings
/// </summary>
public class AudioClipImporterSettings : AssetPostprocessor
{
    #region Unity Methods

    private void OnPostprocessAudio(AudioClip clip)
    {
        var audioImporter = assetImporter as AudioImporter;

        //DefaultのAudioImporterSampleSettings取得
        var settings = audioImporter.defaultSampleSettings;

        //音の長さによって設定し直す
        if (clip.length >= AudioNameCreator.BGM_LENGTH)
        {
            audioImporter.forceToMono = false;

            //ロードしながら再生を行うので、メモリをほんの少ししか使わない
            settings.loadType = AudioClipLoadType.Streaming;
        }
        else
        {
            //大抵の場合はファイルサイズがおよそ半分になる
            //ステレオである必要がない効果音がおすすめ
            audioImporter.forceToMono = true;

            //再生時に展開処理をしなくて済むので、CPUへの負荷が軽くなる
            settings.loadType = AudioClipLoadType.DecompressOnLoad;

            //展開速度が早い、大量に再生する効果音などに使うとCPUの負荷を抑えられる
            settings.compressionFormat = AudioCompressionFormat.ADPCM;
        }
        //変更を反映
        audioImporter.defaultSampleSettings = settings;
    }

    #endregion
}

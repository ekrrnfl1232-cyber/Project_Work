# 현재 씬·프리팹·데이터 연결 (2026-10-05)

정적 YAML에서 자체 스크립트가 연결된 컴포넌트의 전체 직렬화 필드를 추출했다. 외부 모델/텍스처/패키지 원문 및 Unity 플레이 상태는 포함하지 않는다. 프리팹 인스턴스 오버라이드를 포함한 전체 자산 원문은 ChatGPT_Project_TextAssets.zip에 있다.

## Assets/1.Prefab/Boss/AreaAttack.prefab

직접 직렬화된 GameObject 수: 2. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Boss/Boss.prefab

직접 직렬화된 GameObject 수: 34. 자체 스크립트/Canvas 블록 수: 3.

스크립트: Assets/0.Script/0.Game/3.Boss/Boss.cs
```yaml
--- !u!114 &3506866520744508924
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 7689424480026308325}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: c8f07850087c40346b9cc1d41f9e876c, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::Boss
  target: {fileID: 0}
  data: {fileID: 11400000, guid: ca937c554cb118e429f6677f524b4077, type: 2}
  stats: {fileID: 3425897726818796333}
  bossAni: {fileID: 5637188882952814664}
```

스크립트: Assets/0.Script/0.Game/3.Boss/BossStat.cs
```yaml
--- !u!114 &3425897726818796333
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 7689424480026308325}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: d2322b34a4c2457498b007519d6cecb4, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::BossStat
```

스크립트: Assets/0.Script/0.Game/3.Boss/BossView.cs
```yaml
--- !u!114 &5628816050685408952
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 7689424480026308325}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 58dd369a4b326544c9831dcc8e0a4747, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::BossView
  isParent: {fileID: 0}
  prefabHp: {fileID: 4013781562914862402, guid: 6d586452a1caa064c8f27945c1181e52, type: 3}
  areaPrefab: {fileID: 7311808155219869994, guid: 9a95ca2414664524296cd35dac9f8c25, type: 3}
  chargePrefab: {fileID: 6484735945849466092, guid: 00ae224fc7451ab46a5b988eba0aa957, type: 3}
```

## Assets/1.Prefab/Boss/BossHp.prefab

직접 직렬화된 GameObject 수: 4. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Boss/ChargeAttack.prefab

직접 직렬화된 GameObject 수: 2. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Boss/EarthShatter.prefab

직접 직렬화된 GameObject 수: 3. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Boss/SmallExplosion.prefab

직접 직렬화된 GameObject 수: 4. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Entity/[ Door ].prefab

직접 직렬화된 GameObject 수: 3. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/0.Script/0.Game/4.Entity/Door.cs
```yaml
--- !u!114 &1481928883298577552
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1578525275906487971}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 5f3781aec3c0038498dd7f531ece09ae, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::Door
  nextScene: 0
```

## Assets/1.Prefab/Entity/[ NPC ].prefab

직접 직렬화된 GameObject 수: 1. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/0.Script/0.Game/4.Entity/NPC/QuestNPC.cs
```yaml
--- !u!114 &6045792409569898688
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 5439349209792262178}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 377ac6751799cf840ade479d9ac0bc80, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::QuestNPC
  questData: {fileID: 11400000, guid: 4c5014bd49a0fbe46aa1c06ee7f56be9, type: 2}
  questManager: {fileID: 0}
  questUI: {fileID: 0}
  questTitle: {fileID: 0}
  questInfo: {fileID: 0}
```

## Assets/1.Prefab/Entity/Block/Block Wall.prefab

직접 직렬화된 GameObject 수: 1. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Entity/Chests.prefab

직접 직렬화된 GameObject 수: 1. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/0.Script/0.Game/4.Entity/Box.cs
```yaml
--- !u!114 &6187647487631667685
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 2329883563258377849}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 77cec1657bad72d45ad8dbb0aa8fdb8c, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::Box
```

## Assets/1.Prefab/Entity/Ground.prefab

직접 직렬화된 GameObject 수: 83. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Entity/Sensor/Combat_Sensor.prefab

직접 직렬화된 GameObject 수: 1. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/0.Script/0.Game/Dungeon/CombatSensor.cs
```yaml
--- !u!114 &7259620197174645764
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 8163946681398212985}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: b39bce6c6007d9b429c985b47d4e279e, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::CombatSpawn
  combat: {fileID: 0}
  blockWall: {fileID: 0}
```

## Assets/1.Prefab/Entity/Table.prefab

직접 직렬화된 GameObject 수: 1. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Entity/Wall_4M.prefab

직접 직렬화된 GameObject 수: 4. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Entity/Wall_6M.prefab

직접 직렬화된 GameObject 수: 7. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Entity/Wall.prefab

직접 직렬화된 GameObject 수: 29. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Monster/Skeleton.prefab

직접 직렬화된 GameObject 수: 34. 자체 스크립트/Canvas 블록 수: 2.

스크립트: Assets/0.Script/0.Game/2.Monster/Monster.cs
```yaml
--- !u!114 &6755251473351939495
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 6541184085704367737}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 1c2e87209952e0c49807555d4010a428, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::Monster
  target: {fileID: 0}
  qm: {fileID: 0}
  data: {fileID: 11400000, guid: a4e215e7b48a76a459868f0d41f16e61, type: 2}
```

스크립트: Assets/0.Script/0.Game/2.Monster/MonsterView.cs
```yaml
--- !u!114 &7571673112749864989
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 6541184085704367737}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: da17cbc2231d4d245915ed18f4c4340b, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::MonsterView
  isParent: {fileID: 0}
  prefabHP: {fileID: 4013781562914862402, guid: 5d754e21e73a053428ce20e9624f724e, type: 3}
```

## Assets/1.Prefab/Player/[ Player ].prefab

직접 직렬화된 GameObject 수: 64. 자체 스크립트/Canvas 블록 수: 3.

스크립트: Assets/0.Script/0.Game/1.Player/Player.cs
```yaml
--- !u!114 &2288628480039094286
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 3898344367267863381}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: f21e181469068214fbed44152fc10b80, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::Player
  animator: {fileID: 8619422247955481736}
  stat: {fileID: 6565759657930119130}
  data: {fileID: 11400000, guid: 4ac67256bee0ed749b03873637c9af35, type: 2}
  controll: {fileID: 1588461605831524932}
  sword: {fileID: 996706441702685900}
```

스크립트: Assets/0.Script/0.Game/1.Player/PlayerStat.cs
```yaml
--- !u!114 &6565759657930119130
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 3898344367267863381}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: d38d5fc7ec957d045960e6e5f74a1bb6, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::PlayerStat
  data: {fileID: 11400000, guid: 4ac67256bee0ed749b03873637c9af35, type: 2}
  hp: 0
  level: 0
  exp: 0
  maxExp: 0
  baseAttack: 10
  baseDefence: 0
  baseSpeed: 3
  areaDamage: 0
```

스크립트: Assets/0.Script/0.Game/1.Player/PlayerView.cs
```yaml
--- !u!114 &3671470362658505343
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 3898344367267863381}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: ded1a263da9a5fa4592582235acbfda6, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::PlayerView
  area: {fileID: 4581311451610423903}
  isParent: {fileID: 0}
  prefabHP: {fileID: 4013781562914862402, guid: 5d754e21e73a053428ce20e9624f724e, type: 3}
  UiCheckBox: {fileID: 0}
  expImg: {fileID: 0}
  exptext: {fileID: 0}
  animator: {fileID: 8619422247955481736}
  stats: {fileID: 6565759657930119130}
```

## Assets/1.Prefab/Player/AreaAttack.prefab

직접 직렬화된 GameObject 수: 1. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Player/HpBar/MaxHp.prefab

직접 직렬화된 GameObject 수: 2. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/test/Ground_03.prefab

직접 직렬화된 GameObject 수: 1. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Ui/DamageTxt.prefab

직접 직렬화된 GameObject 수: 1. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Ui/Quest UI/QuestAccept.prefab

직접 직렬화된 GameObject 수: 9. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Ui/UI Bar/ExpBG.prefab

직접 직렬화된 GameObject 수: 3. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Ui/UI Bar/GoldBG.prefab

직접 직렬화된 GameObject 수: 4. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Ui/UI Bar/ItemBG.prefab

직접 직렬화된 GameObject 수: 7. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/0.Script/1.UI/Inventory/InventoryItem.cs
```yaml
--- !u!114 &6715487106001312141
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 4692499877091704300}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 6d8bccd357b3e6b44bc23acef90602b3, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::InventoryItem
  iconImg: {fileID: 4798559500635943966}
  nameTxt: {fileID: 8510519662248338156}
  equipImg: {fileID: 4100206709356516380}
  countTxt: {fileID: 7553061234487307422}
  back: {fileID: 2597645267632421500}
```

## Assets/1.Prefab/Ui/UI Bar/MoveItem.prefab

직접 직렬화된 GameObject 수: 6. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/0.Script/1.UI/Inventory/InventoryItem.cs
```yaml
--- !u!114 &4909622402139102661
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 3761704813471429449}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 6d8bccd357b3e6b44bc23acef90602b3, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::InventoryItem
  iconImg: {fileID: 1914901592800000698}
  nameTxt: {fileID: 7089840131306362924}
  equipImg: {fileID: 105769585689629736}
  countTxt: {fileID: 5843058802417861643}
```

## Assets/1.Prefab/Ui/UI Bar/UiCheckBox.prefab

직접 직렬화된 GameObject 수: 2. 자체 스크립트/Canvas 블록 수: 0.

## Assets/1.Prefab/Ui/UI/Inventory UI.prefab

직접 직렬화된 GameObject 수: 14. 자체 스크립트/Canvas 블록 수: 2.

스크립트: Assets/0.Script/1.UI/Inventory/Inventory.cs
```yaml
--- !u!114 &3360396514845502222
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 24957047593683530}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 58b77ac6bb34c094b80f74862556d8c5, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::Inventory
  parent: {fileID: 6490967757325713888}
  invenItem: {fileID: 6715487106001312141, guid: 04eb2d85362ebae4ba4fef56291e02c6, type: 3}
  itemDatas: []
```

스크립트: Assets/0.Script/1.UI/Inventory/InventoryItem.cs
```yaml
--- !u!114 &4289978355472170545
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 2856470332774652496}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 6d8bccd357b3e6b44bc23acef90602b3, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::InventoryItem
  iconImg: {fileID: 2606681578450965410}
  nameTxt: {fileID: 1198049784035722576}
  equipImg: {fileID: 6799871541715317664}
  countTxt: {fileID: 1038922250772237090}
```

## Assets/1.Prefab/Ui/UI/StatSystem.prefab

직접 직렬화된 GameObject 수: 38. 자체 스크립트/Canvas 블록 수: 5.

스크립트: Assets/0.Script/1.UI/Stat/PlusStatSlot.cs
```yaml
--- !u!114 &4595589787480205780
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 684400585910448090}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 4bd9419852e1f6e4f88922e7d9a0491e, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::PlusStatSlot
  plusTxt: {fileID: 1373563337880349675}
  totalTxt: {fileID: 1363399419698687036}
  statUI: {fileID: 7301458299817823448}
```

스크립트: Assets/0.Script/1.UI/Stat/StatUI.cs
```yaml
--- !u!114 &7301458299817823448
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1179466299624643948}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: ce7898b7f03ba2b4bb14c528929ff449, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::PlayerStatUI
  view: {fileID: 7800872492566759179}
  UiStat: {fileID: 8920096477055471889}
  StatSlots:
  - {fileID: 4595589787480205780}
  - {fileID: 2497036409083495373}
  - {fileID: 6138124699367043096}
```

스크립트: Assets/0.Script/1.UI/Stat/StatUIView.cs
```yaml
--- !u!114 &7800872492566759179
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1179466299624643948}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 949cd4ecb16ba6642a1995a5d536127d, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::StatUIView
  playerStat: {fileID: 0}
  stat:
  - {fileID: 3842512435953124015}
  - {fileID: 4224747008944037975}
  - {fileID: 4767180565902674584}
  - {fileID: 5398237425455602004}
  levelStat: {fileID: 5205625895775119134}
  statUI: {fileID: 0}
```

스크립트: Assets/0.Script/1.UI/Stat/PlusStatSlot.cs
```yaml
--- !u!114 &2497036409083495373
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 3213510657165322817}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 4bd9419852e1f6e4f88922e7d9a0491e, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::PlusStatSlot
  plusTxt: {fileID: 8850710902627740123}
  totalTxt: {fileID: 6965150905948142032}
  statUI: {fileID: 7301458299817823448}
```

스크립트: Assets/0.Script/1.UI/Stat/PlusStatSlot.cs
```yaml
--- !u!114 &6138124699367043096
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 3264753309706765633}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 4bd9419852e1f6e4f88922e7d9a0491e, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::PlusStatSlot
  plusTxt: {fileID: 9137832675699320215}
  totalTxt: {fileID: 222798929056322652}
  statUI: {fileID: 7301458299817823448}
```

## Assets/6.Data/BossData/BM.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/6.Data/DataScript/MonsterData.cs
```yaml
--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 8461188ffc9d9e841b9c8f217d0f2cf6, type: 3}
  m_Name: BM
  m_EditorClassIdentifier: Assembly-CSharp::MonsterData
  monsterId: 0
  hp: 500
  damage: 15
  moveSpeed: 3.5
  range: 1.5
  spawnRange: 7
  scanSize: 5
  getExp: 1000
  getGold: 500
```

## Assets/6.Data/ItemData/Gold.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/6.Data/DataScript/ItemScriptable.cs
```yaml
--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: d3c6fee15485809458161d6e280fe99c, type: 3}
  m_Name: Gold
  m_EditorClassIdentifier: Assembly-CSharp::ItemScriptable
  itemID: 4
  itemName: "\uACE8\uB4DC"
  icon: {fileID: 21300000, guid: b938f1e4fa7cca3459bd7aef7f643806, type: 3}
  backgroundIcon: {fileID: 21300000, guid: 29114078ffdf3ad46a63ea39321bcc5b, type: 3}
  maxStack: 4294967290
  price: 0
  damage: 0
  defence: 0
  speed: 0
  itemType: 2
  equipType: 0
```

## Assets/6.Data/MonsterData/MD000.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/6.Data/DataScript/MonsterData.cs
```yaml
--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 8461188ffc9d9e841b9c8f217d0f2cf6, type: 3}
  m_Name: MD000
  m_EditorClassIdentifier: Assembly-CSharp::MonsterData
  monsterId: 1001
  hp: 100
  damage: 5
  moveSpeed: 3.5
  range: 1.5
  spawnRange: 7
  scanSize: 5
  getExp: 2300
  getGold: 50
```

## Assets/6.Data/PlayerData/PD000.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/6.Data/DataScript/PlayerData.cs
```yaml
--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 8486830fbabb53d498c36ce6f5a8a020, type: 3}
  m_Name: PD000
  m_EditorClassIdentifier: Assembly-CSharp::PlayerData
  hp: 100
  maxhp: 100
  jumpHeight: 10
  dashForce: 15
  moveForce: 5
  damage: 10
  areaDmg: 100
  maxExp: 500
  interationScale: 2
```

## Assets/6.Data/QuestData/QuestData 1.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 1.

스크립트: Assets/6.Data/DataScript/QuestData.cs
```yaml
--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 34b15a943faa81d4395a89046896ef33, type: 3}
  m_Name: QuestData 1
  m_EditorClassIdentifier: Assembly-CSharp::QuestData
  questId: 1
  questTitle: "\uB9C8\uC744\uC758 \uC704\uD611"
  description: "\uBAAC\uC2A4\uD130 5\uB9C8\uB9AC\uB97C \uCC98\uC9C0\uD558\uC138\uC694"
  questType: 0
  targetId: 1001
  requiredCount: 5
  rewardGold: 100
  rewardExp: 50
  rewardItem: {fileID: 11400000, guid: e6aa4d8e1c8427f49843b0108dafdf4a, type: 2}
  rewardItemCount: 5
```

## Assets/9.Scenes/Dungeon1.unity

직접 직렬화된 GameObject 수: 224. 자체 스크립트/Canvas 블록 수: 10.

스크립트: Assets/0.Script/9.Addressable/AddressableLoader.cs
```yaml
--- !u!114 &231884181
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 231884180}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 6d79328729394e649b84cbffa0ab7b00, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::AddressableLoader
```

스크립트: Assets/0.Script/0.Game/Dungeon/ObjectPoolManganer.cs
```yaml
--- !u!114 &503914664
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 503914662}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: d880bcfce9720b34eb8356e7da104545, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::ObjectPoolManager
  Enemy: {fileID: 6541184085704367737, guid: bf3147f6299866c4997a51018af7a290, type: 3}
```

스크립트: Assets/0.Script/0.Game/Dungeon/CombatSensor.cs
```yaml
--- !u!114 &857892548
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 857892546}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: b39bce6c6007d9b429c985b47d4e279e, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::CombatSpawn
  spawnArea: {fileID: 1195560103}
```

스크립트: Assets/0.Script/0.Game/Dungeon/CombatSensor.cs
```yaml
--- !u!114 &1020809771
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1020809769}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: b39bce6c6007d9b429c985b47d4e279e, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::CombatSpawn
  spawnArea: {fileID: 888967609}
```

스크립트: Assets/0.Script/0.Game/Camera/ChaseCamera.cs
```yaml
--- !u!114 &1148038970
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1148038965}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 1c066ec6b2879264ca6d8a5a1950214d, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::ChaseCamera
  target: {fileID: 300278502}
  smoothTime: 0.2
```

스크립트: Assets/0.Script/0.Game/Dungeon/MonsterSpawn.cs
```yaml
--- !u!114 &1516752092
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1516752090}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 24abb22592bdf374f8a36a607d602093, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::MonsterSpawn
  combat01: {fileID: 0}
  spawnCount: 5
```

Canvas/Scaler
```yaml
--- !u!114 &1611376896
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1611376894}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 0cd44c1031e13a943bb63640046fad76, type: 3}
  m_Name: 
  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.CanvasScaler
  m_UiScaleMode: 1
  m_ReferencePixelsPerUnit: 100
  m_ScaleFactor: 1
  m_ReferenceResolution: {x: 1920, y: 1080}
  m_ScreenMatchMode: 0
  m_MatchWidthOrHeight: 0
  m_PhysicalUnit: 3
  m_FallbackScreenDPI: 96
  m_DefaultSpriteDPI: 96
  m_DynamicPixelsPerUnit: 1
  m_PresetInfoIsWorld: 0
```

Canvas/Scaler
```yaml
--- !u!223 &1611376897
Canvas:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1611376894}
  m_Enabled: 1
  serializedVersion: 3
  m_RenderMode: 0
  m_Camera: {fileID: 0}
  m_PlaneDistance: 100
  m_PixelPerfect: 0
  m_ReceivesEvents: 1
  m_OverrideSorting: 0
  m_OverridePixelPerfect: 0
  m_SortingBucketNormalizedSize: 0
  m_VertexColorAlwaysGammaSpace: 0
  m_UseReflectionProbes: 0
  m_AdditionalShaderChannelsFlag: 25
  m_UpdateRectTransformForStandalone: 0
  m_SortingLayerID: 0
  m_SortingOrder: 0
  m_TargetDisplay: 0
```

스크립트: Assets/0.Script/0.Game/0.Manager/InputManger.cs
```yaml
--- !u!114 &1725083916
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1725083915}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: c6cc119972bea4e40841b472d64a744b, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::InputManger
```

스크립트: Assets/0.Script/0.Game/0.Manager/Dungeon/DungeonManager.cs
```yaml
--- !u!114 &1788129146
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1788129145}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: fb28c78df2a1f6f4fbc76267660b9c7b, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::DungeonManager
  door: {fileID: 8497128405926237229}
  count: {fileID: 0}
```

## Assets/9.Scenes/Dungeon1/NavMesh-Combat01.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 0.

## Assets/9.Scenes/Dungeon1/NavMesh-Combat02.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 0.

## Assets/9.Scenes/Dungeon1/NavMesh-Combat03 1.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 0.

## Assets/9.Scenes/Dungeon1/NavMesh-Combat03.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 0.

## Assets/9.Scenes/Loading.unity

직접 직렬화된 GameObject 수: 12. 자체 스크립트/Canvas 블록 수: 3.

Canvas/Scaler
```yaml
--- !u!114 &71187917
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 71187915}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 0cd44c1031e13a943bb63640046fad76, type: 3}
  m_Name: 
  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.CanvasScaler
  m_UiScaleMode: 1
  m_ReferencePixelsPerUnit: 100
  m_ScaleFactor: 1
  m_ReferenceResolution: {x: 1920, y: 1080}
  m_ScreenMatchMode: 0
  m_MatchWidthOrHeight: 0
  m_PhysicalUnit: 3
  m_FallbackScreenDPI: 96
  m_DefaultSpriteDPI: 96
  m_DynamicPixelsPerUnit: 1
  m_PresetInfoIsWorld: 0
```

Canvas/Scaler
```yaml
--- !u!223 &71187918
Canvas:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 71187915}
  m_Enabled: 1
  serializedVersion: 3
  m_RenderMode: 0
  m_Camera: {fileID: 0}
  m_PlaneDistance: 100
  m_PixelPerfect: 0
  m_ReceivesEvents: 1
  m_OverrideSorting: 0
  m_OverridePixelPerfect: 0
  m_SortingBucketNormalizedSize: 0
  m_VertexColorAlwaysGammaSpace: 0
  m_UseReflectionProbes: 0
  m_AdditionalShaderChannelsFlag: 25
  m_UpdateRectTransformForStandalone: 0
  m_SortingLayerID: 0
  m_SortingOrder: 100
  m_TargetDisplay: 0
```

스크립트: Assets/0.Script/Loding.cs
```yaml
--- !u!114 &2121685434
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 2121685432}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 97d2905220722ab4db6edfe576540fc7, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::Loding
  loadingbar: {fileID: 649989382}
  loadingText: {fileID: 1883693431}
  loadingImage: {fileID: 0}
```

## Assets/9.Scenes/Lobby.unity

직접 직렬화된 GameObject 수: 57. 자체 스크립트/Canvas 블록 수: 23.

스크립트: Assets/0.Script/1.UI/Quest/QuestUi.cs
```yaml
--- !u!114 &108247126
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 108247125}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: dcd00a972ddaa7c43ab357e3bd3b2ba2, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::QuestUi
  questManager: {fileID: 491513619}
  titleTxt: {fileID: 21969200}
  progressTxt: {fileID: 916183293}
```

스크립트: Assets/0.Script/1.UI/Manager/UIConstroller.cs
```yaml
--- !u!114 &297596604
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 297596603}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 8f0769d62901dc2428c5e9aff8a77a81, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::UIConstroller
  inventory: {fileID: 760268800}
  equipSystem: {fileID: 1372589121}
  moveItem: {fileID: 1543645453}
  Quest: {fileID: 108247126}
  playerStat: {fileID: 416159352}
```

스크립트: Assets/0.Script/0.Game/1.Player/PlayerStat.cs
```yaml
--- !u!114 &416159352 stripped
MonoBehaviour:
  m_CorrespondingSourceObject: {fileID: 6565759657930119130, guid: 85e605427f479844fbbe1da4ad4e39ef, type: 3}
  m_PrefabInstance: {fileID: 416159351}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: d38d5fc7ec957d045960e6e5f74a1bb6, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::PlayerStat
```

스크립트: Assets/0.Script/1.UI/Inventory/EquipmentSlot.cs
```yaml
--- !u!114 &452397476
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 452397473}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 08d8212e6f562574fa70b53301e1a6dc, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::EquipmentSlot
  txtBGObj: {fileID: 215170753}
  iconImg: {fileID: 1726113161}
  itemNameTxt: {fileID: 183289785}
  type: 2
```

스크립트: Assets/0.Script/1.UI/Manager/QuestManager.cs
```yaml
--- !u!114 &491513619
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 491513618}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 3bb0c56923eee1a49bc2cc558770f28e, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::QuestManager
```

Canvas/Scaler
```yaml
--- !u!114 &527660240
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 527660237}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 0cd44c1031e13a943bb63640046fad76, type: 3}
  m_Name: 
  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.CanvasScaler
  m_UiScaleMode: 0
  m_ReferencePixelsPerUnit: 100
  m_ScaleFactor: 1
  m_ReferenceResolution: {x: 800, y: 600}
  m_ScreenMatchMode: 0
  m_MatchWidthOrHeight: 0
  m_PhysicalUnit: 3
  m_FallbackScreenDPI: 96
  m_DefaultSpriteDPI: 96
  m_DynamicPixelsPerUnit: 1
  m_PresetInfoIsWorld: 0
```

Canvas/Scaler
```yaml
--- !u!223 &527660241
Canvas:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 527660237}
  m_Enabled: 1
  serializedVersion: 3
  m_RenderMode: 0
  m_Camera: {fileID: 0}
  m_PlaneDistance: 100
  m_PixelPerfect: 0
  m_ReceivesEvents: 1
  m_OverrideSorting: 0
  m_OverridePixelPerfect: 0
  m_SortingBucketNormalizedSize: 0
  m_VertexColorAlwaysGammaSpace: 0
  m_UseReflectionProbes: 0
  m_AdditionalShaderChannelsFlag: 25
  m_UpdateRectTransformForStandalone: 0
  m_SortingLayerID: 0
  m_SortingOrder: 11
  m_TargetDisplay: 0
```

Canvas/Scaler
```yaml
--- !u!114 &611909686
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 611909682}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 0cd44c1031e13a943bb63640046fad76, type: 3}
  m_Name: 
  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.CanvasScaler
  m_UiScaleMode: 1
  m_ReferencePixelsPerUnit: 100
  m_ScaleFactor: 1
  m_ReferenceResolution: {x: 1920, y: 1080}
  m_ScreenMatchMode: 0
  m_MatchWidthOrHeight: 0.5
  m_PhysicalUnit: 3
  m_FallbackScreenDPI: 96
  m_DefaultSpriteDPI: 96
  m_DynamicPixelsPerUnit: 1
  m_PresetInfoIsWorld: 0
```

Canvas/Scaler
```yaml
--- !u!223 &611909687
Canvas:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 611909682}
  m_Enabled: 1
  serializedVersion: 3
  m_RenderMode: 0
  m_Camera: {fileID: 0}
  m_PlaneDistance: 100
  m_PixelPerfect: 0
  m_ReceivesEvents: 1
  m_OverrideSorting: 0
  m_OverridePixelPerfect: 0
  m_SortingBucketNormalizedSize: 0
  m_VertexColorAlwaysGammaSpace: 0
  m_UseReflectionProbes: 0
  m_AdditionalShaderChannelsFlag: 25
  m_UpdateRectTransformForStandalone: 0
  m_SortingLayerID: 0
  m_SortingOrder: 10
  m_TargetDisplay: 0
```

스크립트: Assets/0.Script/VFX/VFXManager.cs
```yaml
--- !u!114 &675339621
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 675339620}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 237148dfde82e0a4cb898f47ef8a479e, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::VFXManager
  vfxDatas:
  - type: 0
    vfxObj: {fileID: 339158821923109191, guid: 108ca465382c11742bd2c91fe4fdb13f, type: 3}
  - type: 1
    vfxObj: {fileID: 1828176872806910, guid: b596b1825de302d45a96b037393b15b3, type: 3}
  - type: 2
    vfxObj: {fileID: 6469958685643045823, guid: eb16caceb1164c044ad70250e41e591a, type: 3}
```

스크립트: Assets/0.Script/1.UI/Inventory/Inventory.cs
```yaml
--- !u!114 &760268800
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 760268798}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 58b77ac6bb34c094b80f74862556d8c5, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::Inventory
  parent: {fileID: 1497736468}
  invenItem: {fileID: 6715487106001312141, guid: 04eb2d85362ebae4ba4fef56291e02c6, type: 3}
  itemDatas: []
  goldAmount: {fileID: 2065491182}
```

Canvas/Scaler
```yaml
--- !u!114 &767025237
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 767025234}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 0cd44c1031e13a943bb63640046fad76, type: 3}
  m_Name: 
  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.CanvasScaler
  m_UiScaleMode: 1
  m_ReferencePixelsPerUnit: 100
  m_ScaleFactor: 1
  m_ReferenceResolution: {x: 1920, y: 1080}
  m_ScreenMatchMode: 0
  m_MatchWidthOrHeight: 0
  m_PhysicalUnit: 3
  m_FallbackScreenDPI: 96
  m_DefaultSpriteDPI: 96
  m_DynamicPixelsPerUnit: 1
  m_PresetInfoIsWorld: 0
```

Canvas/Scaler
```yaml
--- !u!223 &767025238
Canvas:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 767025234}
  m_Enabled: 1
  serializedVersion: 3
  m_RenderMode: 0
  m_Camera: {fileID: 0}
  m_PlaneDistance: 100
  m_PixelPerfect: 0
  m_ReceivesEvents: 1
  m_OverrideSorting: 0
  m_OverridePixelPerfect: 0
  m_SortingBucketNormalizedSize: 0
  m_VertexColorAlwaysGammaSpace: 0
  m_UseReflectionProbes: 0
  m_AdditionalShaderChannelsFlag: 25
  m_UpdateRectTransformForStandalone: 0
  m_SortingLayerID: 0
  m_SortingOrder: 0
  m_TargetDisplay: 0
```

스크립트: Assets/0.Script/1.UI/Inventory/EquipmentSlot.cs
```yaml
--- !u!114 &850825709
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 850825706}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 08d8212e6f562574fa70b53301e1a6dc, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::EquipmentSlot
  txtBGObj: {fileID: 1569361426}
  iconImg: {fileID: 1486311297}
  itemNameTxt: {fileID: 1306772221}
  type: 4
```

스크립트: Assets/0.Script/1.UI/Inventory/EquipmentSlot.cs
```yaml
--- !u!114 &886673367
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 886673364}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 08d8212e6f562574fa70b53301e1a6dc, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::EquipmentSlot
  txtBGObj: {fileID: 1677375753}
  iconImg: {fileID: 381761198}
  itemNameTxt: {fileID: 1984603193}
  type: 1
```

스크립트: Assets/0.Script/1.UI/Inventory/EquipSystem.cs
```yaml
--- !u!114 &1372589121
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1372589119}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 72a23bb36bca4f644951123d1d8c5940, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::EquipSystem
  slots:
  - {fileID: 886673367}
  - {fileID: 1591452398}
  - {fileID: 452397476}
  - {fileID: 850825709}
```

스크립트: Assets/0.Script/0.Game/4.Entity/NPC/QuestNPC.cs
```yaml
--- !u!114 &1377030268 stripped
MonoBehaviour:
  m_CorrespondingSourceObject: {fileID: 6045792409569898688, guid: 8d0445c42bf8bce4187cbe9773f34d73, type: 3}
  m_PrefabInstance: {fileID: 1498456900}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 377ac6751799cf840ade479d9ac0bc80, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::QuestNPC
```

스크립트: Assets/0.Script/0.Game/0.Manager/InputManger.cs
```yaml
--- !u!114 &1523392826
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1523392824}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: c6cc119972bea4e40841b472d64a744b, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::InputManger
```

스크립트: Assets/0.Script/1.UI/Inventory/InventoryItem.cs
```yaml
--- !u!114 &1543645453 stripped
MonoBehaviour:
  m_CorrespondingSourceObject: {fileID: 4909622402139102661, guid: d315c72ddf66e4745a573f3ee161235e, type: 3}
  m_PrefabInstance: {fileID: 1543645450}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 6d8bccd357b3e6b44bc23acef90602b3, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::InventoryItem
```

스크립트: Assets/0.Script/1.UI/Inventory/EquipmentSlot.cs
```yaml
--- !u!114 &1591452398
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1591452396}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 08d8212e6f562574fa70b53301e1a6dc, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::EquipmentSlot
  txtBGObj: {fileID: 1056836006}
  iconImg: {fileID: 1176606340}
  itemNameTxt: {fileID: 563429232}
  type: 3
```

스크립트: Assets/0.Script/1.UI/DamageFontManager.cs
```yaml
--- !u!114 &1611444896
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1611444894}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: f5f2cb6e9d7086647af89a2cda25e471, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::DamageFontManager
  damageTxt: {fileID: 6018142157182300900, guid: f64571da0c9d2724981fbcae36e00b46, type: 3}
  jumpHeight: 2
  duration: 0.8
```

스크립트: Assets/0.Script/0.Game/0.Manager/SaveManager.cs
```yaml
--- !u!114 &1750292278
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1750292277}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: cca33ab2405a5034295fe0293fb627a4, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::SaveManager
  savePath:
```

스크립트: Assets/0.Script/0.Game/0.Manager/InputManger.cs
```yaml
--- !u!114 &1750292280
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1750292277}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: c6cc119972bea4e40841b472d64a744b, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::InputManger
```

## Assets/9.Scenes/TestRoom.unity

직접 직렬화된 GameObject 수: 27. 자체 스크립트/Canvas 블록 수: 7.

스크립트: Assets/0.Script/0.Game/0.Manager/InputManger.cs
```yaml
--- !u!114 &289030442
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 289030441}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: c6cc119972bea4e40841b472d64a744b, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::InputManger
```

스크립트: Assets/0.Script/0.Game/0.Manager/Dungeon/DungeonManager.cs
```yaml
--- !u!114 &622799114
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 622799112}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: fb28c78df2a1f6f4fbc76267660b9c7b, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::DungeonManager
```

스크립트: Assets/0.Script/0.Game/Dungeon/MonsterSpawn.cs
```yaml
--- !u!114 &1207075750
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1207075748}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 24abb22592bdf374f8a36a607d602093, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::MonsterSpawn
  spawnCount: 10
  target: {fileID: 870098518}
  data: {fileID: 11400000, guid: a4e215e7b48a76a459868f0d41f16e61, type: 2}
```

스크립트: Assets/0.Script/0.Game/Dungeon/ObjectPoolManganer.cs
```yaml
--- !u!114 &1602493577
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1602493576}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: d880bcfce9720b34eb8356e7da104545, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::ObjectPoolManager
  uiParent: {fileID: 1674686711}
  Enemy: {fileID: 6541184085704367737, guid: bf3147f6299866c4997a51018af7a290, type: 3}
  Hpbar: {fileID: 4013781562914862402, guid: 5d754e21e73a053428ce20e9624f724e, type: 3}
```

Canvas/Scaler
```yaml
--- !u!114 &1674686709
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1674686707}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 0cd44c1031e13a943bb63640046fad76, type: 3}
  m_Name: 
  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.CanvasScaler
  m_UiScaleMode: 1
  m_ReferencePixelsPerUnit: 100
  m_ScaleFactor: 1
  m_ReferenceResolution: {x: 1920, y: 1080}
  m_ScreenMatchMode: 0
  m_MatchWidthOrHeight: 0
  m_PhysicalUnit: 3
  m_FallbackScreenDPI: 96
  m_DefaultSpriteDPI: 96
  m_DynamicPixelsPerUnit: 1
  m_PresetInfoIsWorld: 0
```

Canvas/Scaler
```yaml
--- !u!223 &1674686710
Canvas:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1674686707}
  m_Enabled: 1
  serializedVersion: 3
  m_RenderMode: 0
  m_Camera: {fileID: 0}
  m_PlaneDistance: 100
  m_PixelPerfect: 0
  m_ReceivesEvents: 1
  m_OverrideSorting: 0
  m_OverridePixelPerfect: 0
  m_SortingBucketNormalizedSize: 0
  m_VertexColorAlwaysGammaSpace: 0
  m_UseReflectionProbes: 0
  m_AdditionalShaderChannelsFlag: 25
  m_UpdateRectTransformForStandalone: 0
  m_SortingLayerID: 0
  m_SortingOrder: 0
  m_TargetDisplay: 0
```

스크립트: Assets/0.Script/0.Game/Camera/ChaseCamera.cs
```yaml
--- !u!114 &1804936722
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1804936717}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 1c066ec6b2879264ca6d8a5a1950214d, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::CameraChase
  target: {fileID: 870098518}
  smoothTime: 0.2
```

## Assets/9.Scenes/TestRoom/NavMesh-NavMesh Surface.asset

직접 직렬화된 GameObject 수: 0. 자체 스크립트/Canvas 블록 수: 0.

## Assets/9.Scenes/Title.unity

직접 직렬화된 GameObject 수: 8. 자체 스크립트/Canvas 블록 수: 3.

스크립트: Assets/0.Script/SceneLoader.cs
```yaml
--- !u!114 &721571340
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 721571339}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 90cb0ca07d319a4419823999bd46c6a8, type: 3}
  m_Name: 
  m_EditorClassIdentifier: Assembly-CSharp::SceneLoader
```

Canvas/Scaler
```yaml
--- !u!114 &1337631565
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1337631563}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: 0cd44c1031e13a943bb63640046fad76, type: 3}
  m_Name: 
  m_EditorClassIdentifier: UnityEngine.UI::UnityEngine.UI.CanvasScaler
  m_UiScaleMode: 1
  m_ReferencePixelsPerUnit: 100
  m_ScaleFactor: 1
  m_ReferenceResolution: {x: 1920, y: 1080}
  m_ScreenMatchMode: 0
  m_MatchWidthOrHeight: 0
  m_PhysicalUnit: 3
  m_FallbackScreenDPI: 96
  m_DefaultSpriteDPI: 96
  m_DynamicPixelsPerUnit: 1
  m_PresetInfoIsWorld: 0
```

Canvas/Scaler
```yaml
--- !u!223 &1337631566
Canvas:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 1337631563}
  m_Enabled: 1
  serializedVersion: 3
  m_RenderMode: 0
  m_Camera: {fileID: 0}
  m_PlaneDistance: 100
  m_PixelPerfect: 0
  m_ReceivesEvents: 1
  m_OverrideSorting: 0
  m_OverridePixelPerfect: 0
  m_SortingBucketNormalizedSize: 0
  m_VertexColorAlwaysGammaSpace: 0
  m_UseReflectionProbes: 0
  m_AdditionalShaderChannelsFlag: 25
  m_UpdateRectTransformForStandalone: 0
  m_SortingLayerID: 0
  m_SortingOrder: 0
  m_TargetDisplay: 0
```


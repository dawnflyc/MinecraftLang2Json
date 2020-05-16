## 这是一个**lang**文件转换为json文件的工具。

用于Minecraft（简称mc）1.12版本语言文件（使用lang）转为1.13或者以后版本的语言文件（使用json）

但是呢，因为其他原因，有些词条并不一致，所以还需要稍微改一改，本软件只是辅助。



**转换演示：**

转换前：

`minecraft.item.air=空气`

转换后：

`{`

`“minecraft.item.air”：“空气”`

`}`

**版本不一致，词条经常会这样：***(注意符号“.”和":")*

1.12：

minecraft.item**:**air=空气

1.13:

`{`

`“minecraft.item.air”：“空气”`

`}`
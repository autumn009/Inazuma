Inazuma Procedural Text Editor
==============================
Version 1.0.0.1
2021�N6��19��
�얓 �� (autumn)

�@�y���ꂪ�������G�f�B�^��!�z

�� �T�v
�@grep�Ȃǂ̃R�}���h���C���E�c�[����p���ăe�L�X�g��ҏW���邽�߂́A�葱���^�̃e�L�X�g�G�f�B�^(Procedural Text Editor)�ł��B
�@�ʏ�̎蓮�ҏW��O��Ƃ����G�f�B�^�Ƃ͑S�������̈قȂ钴�����G�f�B�^�ł��B�]���^�G�f�B�^�̋@�\�⊮�p�ł��B

�� �@�\
�@�e�L�X�g�{�b�N�X�̓��e��W�����͂Ƃ��A�C�ӂ̃R�}���h�����s���A�W���o��/�G���[�o�͂̓��e��ʂ̃e�L�X�g�{�b�N�X�ɏ����o���܂��B
�@���o�̓f�[�^���t�@�C���ɏ������ނ��ƂȂ��A�R�}���h���C���c�[�������s�ł��܂��B
�@grep��sort|uniq��GUI�c�[���ƘA�g���邽�߂ɍ쐬����Ă��܂��B
�@(grep�Ȃǂ͕t�����܂���)
�@�t�@�C���ւ̓��o�͂̋@�\�͎����Ă��܂���B�N���b�v�{�[�h�o�R�Ńe�L�X�g���󂯎��A�N���b�v�{�[�h�o�R�Ō��ʂ����o���܂��B

�� �����
Windows 10/.NET 5
(Windows�ȊO�ł͓��삵�܂���)

�� ��{�I�Ȏg����
�@���C���E�B���h�E��Command�̃e�L�X�g�{�b�N�X�Ɏ��s����s����͂��܂��B(��ƃf�B���N�g���̃t�@�C���ꗗ�𓾂�Ȃ�"dir")
�@���s����s�́A�W�����͂�����͂�(�I�v�V����)�A�W���o�͂ɏo�͂���K�v������܂��B
�@�g�p����G���R�[�f�B���O��UTF-8���f�t�H���g�ł����A�������K�V�[�ȃG���R�[�f�B���O(�V�t�gJIS�Ȃ�)�����g���Ȃ��c�[�����N������ꍇ��"DEF ENC"�Ƀ`�F�b�N�����܂��B
�@�㑤�̕����s�̃e�L�X�g�{�b�N�X�ɓ��̓e�L�X�g����͂��ARUN�{�^���������܂��B����ƁA�����̕����s�̃e�L�X�g�{�b�N�X�Ɍ��ʂ��\������܂��B
�@�����A�㑤�̕����s�̃e�L�X�g�{�b�N�X����̏ꍇ�͎��s�ɐ旧���ăN���b�v�{�[�h����\��t�����s���܂��B

�� �g�p���@�E���C���E�B���h�E
Macro�@��`�ς݂̃}�N����I�����܂��B�I�����ꂽ�}�N���̓��e�́ACommand����DEF ENC���ɓ���܂��B
Command�@���s����s����͂��܂�
DEF ENC�@�V�X�e���f�t�H���g�̃��K�V�[�G���R�[�f�B���O(���{�Ȃ�V�t�gJIS/CP932)�Ŏ��s����ꍇ�̓`�F�b�N�����܂��B�`�F�b�N�����Ȃ��ꍇ��UTF-8/CP 65001�Ŏ��s����܂�)
Edit Macro ��`�ς݂̃}�N����ҏW���܂��B��`�ς݃}�N���͂��̋@�\���g��Ȃ�����ύX����܂���B(�܂�ACommand���͍D���Ȃ悤�ɏ��������Ă��e���͎c��܂���)
Working Dir ���[�L���O�f�B���N�g��(�J�����g�f�B���N�g��)��ύX���܂��B
Paste Source�@�㑤�̕����s�̃e�L�X�g�{�b�N�X�ɃN���b�v�{�[�h�̃e�L�X�g��\��t���܂�
Run���@�R�}���h�����s���܂��@(Enter�L�[�������Ă����̃{�^�����@�\���܂�)
Copy Result�@�����̕����s�̃e�L�X�g�{�b�N�X�ɓ����Ă���e�L�X�g���N���b�v�{�[�h�ɃR�s�[���܂��B

�� �g�p���@�E�}�N���ҏW�E�B���h�E
Macros �C������}�N����I�����܂�
Name �}�N���̖��O����͂��܂�
Command ���s����R�}���h���C������͂��܂�
Use System Default Legacy Encoding�@�V�X�e���f�t�H���g�̃��K�V�[�G���R�[�f�B���O(���{�Ȃ�V�t�gJIS/CP932)���g�p����ꍇ�̓`�F�b�N���܂�
Add New Entry�@�V�����}�N����ǉ����܂�
Remove Entry�@�I�����ꂽ�}�N�����폜���܂�
OK�@�C���𔽉f���ă_�C�A���O�{�b�N�X����܂�
Cancel�@�C����j�����ă_�C�A���O�{�b�N�X����܂�

�� ��`�ς݃}�N���̉��
�@�}�N���̒�`�����݂��Ȃ����A�ȉ��̃}�N���������I�ɒ�`����܂��B

grep, grep�����s���܂��BSEARCH TEXT�܂��̓X�N���v�g�S�̂����������Ďg�p���܂��B
sed, sed�����s���܂��BSEARCH_FOR/REPlACE_WITH�܂��̓X�N���v�g�S�̂����������Ďg�p���܂��B
awk, awk�����s���܂��BSEARCH_FOR�܂��̓X�N���v�g�S�̂����������Ďg�p���܂��B
sort|uniq, sort|uniq�����s���܂��B
Current Directory, CD�����s���܂��B
File Association, ASSOC�����s���܂��B
Command Search Path, PATH�����s���܂��B
List Env Var, SET�����s���܂��B
SORT, SORT�����s���܂��B
System Info, SYSTEMINFO�����s���܂��B
Task List, TASKLIST�����s���܂��B
Directory, Tree TREE�����s���܂��B
Windows Version, VER�����s���܂��B
Volume Name, VOL�����s���܂��B
Find String, FIND "SEARCH TEXT"�����s���܂��BSEARCH TEXT�܂��̓X�N���v�g�S�̂����������Ďg�p���܂��B
Welcome, echo Welcome to Inazuma Procedural Text Editor, please re-write this command to learn how to use.");

��:�@uniq/grep/sed/awk�͕t�����܂���B���D�݂̂��̂����g���������B���̃c�[����Windows�W���t���ł��B

�� �}�N���̐ݒ�t�@�C���̃p�X
C:\Users\(user name)\AppData\Roaming\Pie Dey\Inazuma\macros.xml

�� ������̐���
�@cmd.exe���s���Ƀ��W�X�g�������������ăf�t�H���g�̃R�[�h�y�[�W��ύX���Ă��܂��B�ύX���Ɏ蓮��cmd.exe�����s����ƒʏ�̃f�t�H���g�ȊO�̃R�[�h�y�[�W��cmd.exe���N�����܂��B�܂��{�\�t�g���������f�����ƃ��W�X�g���Ɉꎞ�I�Ȓl���c��\��������܂��B
�@�ύX���郌�W�X�g���́AHKEY_CURRENT_USER��"Console\%SystemRoot%_system32_cmd.exe"��CodePage�T�u�L�[�̒l�ł��B
�@�܂��A�\����A�W�����͂Ƀ��_�C���N�g����Ȃ��L�[���͑҂��̂���R�}���h�����s����ƃn���O���܂��B
�@���o�͂̃e�L�X�g�{�b�N�X�̗e�ʂ̐��񂩂炠�܂�傫�ȃf�[�^�͈����܂���B

�� �\�[�X�R�[�h
�@�\�[�X�R�[�h��github�őS�Č��J����Ă��܂��B
https://github.com/autumn009/Inazuma

�� �T�|�[�g
Twitter @KawamataAkira
Facebook autumn009
�d�q���[�� autumn@piedey.co.jp
�얓�����ɂ��₢���킹������

�� ���C�Z���X
MIT License (Open Source)

�� ���
�@���́A8bit����͂���Ȃ�ɕ֗��ȃG�f�B�^���J�����Ďg�p�������Ƃ�����܂��B���̂��߁A���Ȃ�̂�������ƍ��x�ȃe�L�X�g�G�f�B�^�̊J�����l���Ă��܂����B�������AWindows����ɓ������Ƃ��Ɂy�G�ہz�̑��݂�m���āA�G�f�B�^�̎s���H�������̂�����ł͂Ȃ��ƍl���ăG�f�B�^�̊J���͐摗�肵�܂����B�������A���낻�돑���Ă��ǂ����ȂƎv���Ă��܂����B�������A��Ԃ͔��ɑ傫�Ȃ��̂ɂȂ�܂��B�{�G�f�B�^�́A�{���\�z���ꂽ�V�F���@�\���܂ފ��S�ȃe�L�X�g�G�f�B�^�̍\�z�̂����A�V�F���@�\�������s���Ď����������̂ł��B���R�͊ȒP�ŁA���z�̃V�F���@�\�ȊO�͓��ɑ��̃c�[���ŗp�����肽����ł��B���͎d����sort|uniq�����s����񐔂����Ƒ����Ȃ������̂́A���������R�}���h�v�����v�g���J����Ԃ��ʓ|�ɂȂ���Inazuma���쐬���܂����B�܂�͎����̂��߂ł��B�J���r���̔ł͊m���ɖ𗧂��Ă��܂����B�܂�AInazuma�̓C�i�Y�}���Ƃ��������܂��������̃T�i�M�}���ł��B

�� ��������
1.0.0.1�@2021�N6��19���@�ŏ��̃����[�X

�ȏ�

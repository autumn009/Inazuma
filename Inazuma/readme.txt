Inazuma Procedural Text Editor
==============================
Version 1.0.0.1
2021�N6��19��

�@���ꂪ�������G�f�B�^��!

�� �T�v
�@grep�Ȃǂ̃R�}���h���C���E�c�[���Ńe�L�X�g��ҏW���邽�߂́A�葱���^�̃e�L�X�g�G�f�B�^(Procedural Text Editor)�ł��B

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
�@�����A���K�V�[�ȃG���R�[�f�B���O�����g���Ȃ��c�[�����N������ꍇ��"DEF ENC"�Ƀ`�F�b�N�����܂��B
�@�㑤�̕����s�̃e�L�X�g�{�b�N�X�ɓ��̓e�L�X�g����͂��ARUN�{�^���������܂��B����ƁA�����̕����s�̃e�L�X�g�{�b�N�X�Ɍ��ʂ��\������܂��B
�@�����A�㑤�̕����s�̃e�L�X�g�{�b�N�X����̏ꍇ�͎��s�ɐ旧���ăN���b�v�{�[�h����\��t�����s���܂��B

�� �g�p���@�E���C���E�B���h�E
Macro�@��`�ς݂̃}�N����I�����܂��B�I�����ꂽ�}�N���̃R�}���h���C���́ACommand����DEF ENC���ɓ���܂��B
Command�@���s����s����͂��܂�
DEF ENC�@�V�X�e���f�t�H���g�̃��K�V�[�G���R�[�f�B���O(���{�Ȃ�V�t�gJIS/CP932)�Ŏ��s����ꍇ�̓`�F�b�N�����܂��B�`�F�b�N�����Ȃ��ꍇ��UTF-8/CP 65001�Ŏ��s����܂�)
Edit Macro ��`�ς݂̃}�N����ҏW���܂��B��`�ς݃}�N���͂��̋@�\���g��Ȃ�����ύX����܂���B(�܂�ACommand���͍D���Ȃ悤�ɏ��������Ă��e���͎c��܂���)
Working Dir ���[�L���O�f�B���N�g��(�J�����g�f�B���N�g��)��ύX���܂��B
Paste Source�@�㑤�̕����s�̃e�L�X�g�{�b�N�X�ɃN���b�v�{�[�h�̃e�L�X�g��\��t���܂�
Run���@�R�}���h�����s���܂�
Copy Result�@�����̕����s�̃e�L�X�g�{�b�N�X�ɓ����Ă���e�L�X�g���N���b�v�{�[�h�ɃR�s�[���܂��B

�� �g�p���@�E�}�N���ҏW�E�B���h�E
Use System Default Legacy Encoding�@�V�X�e���f�t�H���g�̃��K�V�[�G���R�[�f�B���O(���{�Ȃ�V�t�gJIS/CP932)���g�p����ꍇ�̓`�F�b�N���܂�
Macros �C������}�N����I�����܂�
Name ���O����͂��܂�
Command ���s����R�}���h���C������͂��܂�
Add New Entry�@�V�����}�N����ǉ����܂�
Remove Entry�@�I�����ꂽ�}�N�����폜���܂�
OK�@�C���𔽉f���ă_�C�A���O�{�b�N�X����܂�
Cancel�@�C����j�����ă_�C�A���O�{�b�N�X����܂�

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
�d�q���[�� autumn@piedey.co.jp
�얓�����ɂ��₢���킹������

�� ���C�Z���X
MIT License (Open Source)

�� ��������
1.0.0.1�@2021�N6��19���@�ŏ��̃����[�X

�ȏ�

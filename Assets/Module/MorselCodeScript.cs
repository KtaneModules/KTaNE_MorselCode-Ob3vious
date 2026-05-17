using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class MorselCodeScript : MonoBehaviour
{
    public static string LetterGrid = "JVFHTQBLNAUXWKODYZGRISMPC";
    public static Dictionary<char, string> Codes = new Dictionary<char, string>
    {
        { 'E', "."},
        { 'T', ".."},
        { 'A', ". ."},
        { 'O', ". .."},
        { 'I', ".. ."},
        { 'N', ". . ."},
        { 'S', ".. .."},
        { 'H', ". . .."},
        { 'R', ". .. ."},
        { 'D', ".. . ."},
        { 'L', ". . . ."},
        { 'C', ". .. .."},
        { 'U', ".. . .."},
        { 'M', ".. .. ."},
        { 'W', ". . . .."},
        { 'F', ". . .. ."},
        { 'G', ". .. . ."},
        { 'Y', ".. . . ."},
        { 'P', ".. .. .."},
        { 'B', ". . . . ."},
        { 'V', ". . .. .."},
        { 'K', ". .. . .."},
        { 'J', ". .. .. ."},
        { 'X', ".. . . .."},
        { 'Q', ".. . .. ."},
        { 'Z', ".. .. . ."}
    };
    public static List<WordCluster> WordClusters = new List<WordCluster>
    {
        new WordCluster("URNEBA", new string[][]{new string[]{"RUNE"}, new string[]{"BURN"}, new string[]{"RAUN"}, new string[]{"RUBE"}, new string[]{"UREA"}, new string[]{"BAUR", "BURA"}, new string[]{"UNBE"}, new string[]{"AUNE"}, new string[]{"BUNA"}, new string[]{"BEAU"}, new string[]{"BREN"}, new string[]{"EARN", "NARE", "NEAR", "REAN"}, new string[]{"BARN", "BRAN"}, new string[]{"ABER", "BARE", "BEAR", "BRAE"}, new string[]{"BANE", "BEAN", "NABE"}}),
        new WordCluster("RKIEDA", new string[][]{new string[]{"KEIR", "KIER", "REIK"}, new string[]{"DIRK"}, new string[]{"KRAI", "RAIK", "RAKI"}, new string[]{"DREK"}, new string[]{"RAKE", "REAK"}, new string[]{"DARK"}, new string[]{"DIRE", "IRED", "RIDE"}, new string[]{"ARIE"}, new string[]{"ARID", "DARI", "RAID", "RIAD"}, new string[]{"ARED", "DARE", "DEAR", "EARD", "RADE", "READ"}, new string[]{"DIKE"}, new string[]{"KAIE"}, new string[]{"DIKA", "KADI", "KAID"}, new string[]{"AKED", "KADE", "KAED"}, new string[]{"AIDE", "IDEA"}}),
        new WordCluster("TOLEDA", new string[][]{new string[]{"LOTE", "TOLE"}, new string[]{"DOLT", "TOLD"}, new string[]{"ALTO", "LOTA", "TOLA"}, new string[]{"DOTE", "TOED"}, new string[]{"TOEA"}, new string[]{"DATO", "DOAT", "TOAD"}, new string[]{"DELT", "TELD"}, new string[]{"LATE", "LEAT", "TAEL", "TALE", "TEAL", "TELA"}, new string[]{"DALT"}, new string[]{"DATE", "TAED", "TEAD"}, new string[]{"DELO", "DOLE", "LODE", "OLDE"}, new string[]{"ALOE", "OLEA"}, new string[]{"ALOD", "LOAD", "ODAL"}, new string[]{"ODEA"}, new string[]{"DALE", "DEAL", "LADE", "LEAD"}}),
        new WordCluster("YSREDA", new string[][]{new string[]{"RYES"}, new string[]{"DRYS"}, new string[]{"ARSY", "RAYS", "RYAS"}, new string[]{"DEYS", "DYES", "SYED"}, new string[]{"AYES", "EASY", "EYAS", "YEAS"}, new string[]{"DAYS", "YADS"}, new string[]{"DREY", "DYER", "YERD"}, new string[]{"AERY", "AYRE", "EYRA", "YARE", "YEAR"}, new string[]{"ADRY", "DRAY", "YARD"}, new string[]{"YEAD"}, new string[]{"REDS"}, new string[]{"ARES", "ARSE", "EARS", "ERAS", "RASE", "SEAR", "SERA"}, new string[]{"ARDS", "RADS", "SARD"}, new string[]{"DAES", "SADE"}, new string[]{"ARED", "DARE", "DEAR", "EARD", "RADE", "READ"}}),
        new WordCluster("URNGEA", new string[][]{new string[]{"GURN", "RUNG"}, new string[]{"RUNE"}, new string[]{"RAUN"}, new string[]{"GRUE", "URGE"}, new string[]{"GAUR", "GUAR", "RAGU", "RUGA"}, new string[]{"UREA"}, new string[]{"GENU"}, new string[]{"GAUN", "GUAN"}, new string[]{"AUNE"}, new string[]{"AGUE"}, new string[]{"GREN"}, new string[]{"GNAR", "GRAN", "RANG"}, new string[]{"EARN", "NARE", "NEAR", "REAN"}, new string[]{"AGER", "AREG", "GARE", "GEAR", "RAGE"}, new string[]{"AGEN", "GAEN", "GANE", "GEAN", "GENA"}}),
        new WordCluster("USRGEA", new string[][]{new string[]{"GURS", "RUGS"}, new string[]{"RUES", "RUSE", "SUER", "SURE", "URES", "USER"}, new string[]{"RUSA", "SURA", "URSA"}, new string[]{"GUES"}, new string[]{"GAUS"}, new string[]{"EAUS"}, new string[]{"GRUE", "URGE"}, new string[]{"GAUR", "GUAR", "RAGU", "RUGA"}, new string[]{"UREA"}, new string[]{"AGUE"}, new string[]{"ERGS", "GERS", "REGS"}, new string[]{"GARS", "RAGS"}, new string[]{"ARES", "ARSE", "EARS", "ERAS", "RASE", "SEAR", "SERA"}, new string[]{"AGES", "GAES", "SAGE"}, new string[]{"AGER", "AREG", "GARE", "GEAR", "RAGE"}}),
        new WordCluster("RMKIEA", new string[][]{new string[]{"MIRK"}, new string[]{"MERK"}, new string[]{"MARK"}, new string[]{"EMIR", "MERI", "MIRE", "RIEM", "RIME"}, new string[]{"AMIR", "MAIR", "RAMI", "RIMA"}, new string[]{"MARE", "REAM"}, new string[]{"KEIR", "KIER", "REIK"}, new string[]{"KRAI", "RAIK", "RAKI"}, new string[]{"RAKE", "REAK"}, new string[]{"ARIE"}, new string[]{"MIKE"}, new string[]{"KAIM", "KAMI", "MAIK", "MAKI"}, new string[]{"KAME", "MAKE"}, new string[]{"AMIE"}, new string[]{"KAIE"}}),
        new WordCluster("RNKIEA", new string[][]{new string[]{"KIRN", "RINK"}, new string[]{"KERN", "NERK", "RENK"}, new string[]{"KARN", "KNAR", "NARK", "RANK"}, new string[]{"REIN", "RINE"}, new string[]{"AIRN", "RAIN", "RANI"}, new string[]{"EARN", "NARE", "NEAR", "REAN"}, new string[]{"KEIR", "KIER", "REIK"}, new string[]{"KRAI", "RAIK", "RAKI"}, new string[]{"RAKE", "REAK"}, new string[]{"ARIE"}, new string[]{"KINE"}, new string[]{"AKIN", "IKAN", "KAIN", "KINA", "NAIK"}, new string[]{"KANE"}, new string[]{"AINE", "EINA"}, new string[]{"KAIE"}}),
        new WordCluster("TRONEA", new string[][]{new string[]{"RONT", "TORN", "TRON"}, new string[]{"ROTE", "TORE"}, new string[]{"RATO", "ROTA", "TARO", "TORA"}, new string[]{"RENT", "TERN"}, new string[]{"RANT", "TARN"}, new string[]{"ARET", "RATE", "TARE", "TEAR"}, new string[]{"NOTE", "TONE"}, new string[]{"NOTA"}, new string[]{"TOEA"}, new string[]{"ANTE", "ETNA", "NEAT", "TANE"}, new string[]{"ONER", "RENO", "RONE"}, new string[]{"ROAN", "RONA"}, new string[]{"AERO"}, new string[]{"EARN", "NARE", "NEAR", "REAN"}, new string[]{"AEON", "EOAN"}}),
        new WordCluster("YSROKA", new string[][]{new string[]{"ROSY"}, new string[]{"SKRY", "SKYR"}, new string[]{"ARSY", "RAYS", "RYAS"}, new string[]{"YOKS"}, new string[]{"SOYA"}, new string[]{"KAYS", "YAKS"}, new string[]{"ROKY", "YORK"}, new string[]{"OARY"}, new string[]{"KRAY", "KYAR", "YARK"}, new string[]{"KAYO", "OAKY", "OKAY"}, new string[]{"KORS", "ROKS"}, new string[]{"AROS", "OARS", "OSAR", "SOAR", "SORA"}, new string[]{"ARKS", "KSAR", "SARK"}, new string[]{"KOAS", "OAKS", "OKAS", "SOAK"}, new string[]{"KARO", "KORA", "OKRA"}}),
        new WordCluster("UTROMA", new string[][]{new string[]{"ROUT", "TOUR", "TROU"}, new string[]{"TURM"}, new string[]{"RATU"}, new string[]{"MOTU"}, new string[]{"AUTO", "OUTA"}, new string[]{"MAUT"}, new string[]{"ROUM"}, new string[]{"URAO"}, new string[]{"ARUM", "MURA", "UMRA"}, new string[]{"OUMA"}, new string[]{"MORT"}, new string[]{"RATO", "ROTA", "TARO", "TORA"}, new string[]{"MART", "TRAM"}, new string[]{"ATOM", "MOAT"}, new string[]{"MORA", "ROAM", "ROMA"}}),
        new WordCluster("YSROED", new string[][]{new string[]{"ROSY"}, new string[]{"RYES"}, new string[]{"DRYS"}, new string[]{"OYES"}, new string[]{"DOYS", "YODS"}, new string[]{"DEYS", "DYES", "SYED"}, new string[]{"OYER", "YORE"}, new string[]{"DORY"}, new string[]{"DREY", "DYER", "YERD"}, new string[]{"YODE"}, new string[]{"EROS", "ORES", "REOS", "ROES", "ROSE", "SORE"}, new string[]{"DORS", "ORDS", "RODS", "SORD"}, new string[]{"REDS"}, new string[]{"DOES", "DOSE", "ODES"}, new string[]{"DERO", "DOER", "DORE", "REDO", "RODE", "ROED"}}),
        new WordCluster("YSROKE", new string[][]{new string[]{"ROSY"}, new string[]{"SKRY", "SKYR"}, new string[]{"RYES"}, new string[]{"YOKS"}, new string[]{"OYES"}, new string[]{"ESKY", "KEYS", "KYES", "SYKE", "YESK"}, new string[]{"ROKY", "YORK"}, new string[]{"OYER", "YORE"}, new string[]{"RYKE", "YERK"}, new string[]{"YOKE"}, new string[]{"KORS", "ROKS"}, new string[]{"EROS", "ORES", "REOS", "ROES", "ROSE", "SORE"}, new string[]{"ERKS", "SERK", "SKER"}, new string[]{"OKES", "SKEO", "SOKE"}, new string[]{"KERO", "KORE", "ROKE"}}),
    };
    public const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    //ruleseed
    private string _letterBank;
    private List<string> _words;
    private string _alphabetMap;

    private int _position;
    private string _chosenWordArrangement;
    private char _goalChar;
    private List<int> _letterComparisons;

    public Transform ModuleBg;
    public Transform StatusLightParent;
    public Transform InputCasing;
    public Transform OutputCasing;
    public KMSelectable Input;
    public MeshRenderer Output;
    public Material[] LedMaterials;

    private bool _held = false;
    private List<float> _inputs = new List<float>();
    private float _lastInput = 0;

    private bool _isSolved = false;
    private int _moduleId = 0;
    private static int _moduleIdCounter = 1;


    void Start()
    {
        _isSolved = false;
        _moduleId = _moduleIdCounter++;

        SetRuleset();
        //Debug.Log(_alphabetMap);
        //Debug.Log(_letterBank);
        //Debug.Log(_words.Join(", "));
        //Debug.Log(_letterComparisons.Join(", "));

        SetPuzzle();
        SetVisuals();

        StartCoroutine(KeyHandler());
        StartCoroutine(LightHandler());

        Input.OnInteract += () =>
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, InputCasing);
            Input.AddInteractionPunch(0.5f);
            if (_lastInput < 0)
                return false;
            if (_held)
                return false;
            _held = true;
            if (_isSolved)
                return false;
            Signal();
            return false;
        };

        Input.OnInteractEnded += () =>
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, InputCasing);
            if (_lastInput < 0)
                return;
            if (!_held)
                return;
            _held = false;
            if (_isSolved)
                return;
            Signal();
        };
    }



    void Update()
    {
        if (_isSolved)
            return;

        _lastInput += Time.deltaTime;

        List<char> potentials = VerifyInputs();
        if (potentials == null)
        {
            HandleInvalidSignal();
        }

        if (_inputs.Count > 1 && !_held && _inputs.Skip(1).Min() < _lastInput / 4)
        {
            Log("The signal transmitted can be interpreted as: {0}.", potentials.Count == 0 ? "none" : potentials.Join("/"));

            _inputs.Clear();
            if (potentials.Contains(_goalChar))
            {
                Log("The signal can be matched to the expected signal. Module solved!");
                _isSolved = true;
                GetComponent<KMBombModule>().HandlePass();
            }
            else
            {
                Log("The signal could not be matched to the expected signal. Module solved!");
                GetComponent<KMBombModule>().HandleStrike();
            }
        }
    }

    private IEnumerator KeyHandler()
    {
        float pressDepth = 0;
        while (true)
        {
            if (_held && pressDepth < 1)
                pressDepth += Time.deltaTime * 16;
            if (!_held && pressDepth > 0)
                pressDepth -= Time.deltaTime * 16;
            pressDepth = Mathf.Clamp01(pressDepth);
            Input.transform.parent.localEulerAngles = new Vector3(Mathf.Lerp(0f, 7.5f, pressDepth), 0, 0);
            yield return null;
        }
    }

    private IEnumerator LightHandler()
    {
        float unitLength = 0.25f;
        float timer = 0;
        string sequence = "    " + _chosenWordArrangement.Select(x => Morsify(x)).Join("  ");
        int index = 0;
        while (!_isSolved)
        {
            if (_inputs.Count > 0)
            {
                index = 0;
                timer = 0;
                Output.material = LedMaterials[1];
            }
            else
                Output.material = LedMaterials[sequence[index] == ' ' ? 0 : 1];

            yield return null;
            timer += Time.deltaTime;
            while (timer > unitLength)
            {
                index++;
                timer -= unitLength;
            }
            index %= sequence.Length;
        }
        Output.material = LedMaterials[0];
    }

    private void SetRuleset()
    {

        MonoRandom ruleSeed = GetComponent<KMRuleSeedable>().GetRNG();
        Log("The ruleseed for this bomb is {0}.", ruleSeed.Seed);

        _alphabetMap = ruleSeed.ShuffleFisherYates(ALPHABET.ToList()).Join("");

        WordCluster selectedCluster = WordClusters[ruleSeed.Next(0, WordClusters.Count)];
        _letterBank = ruleSeed.ShuffleFisherYates(selectedCluster.Letterbank.ToList()).Join("");
        _words = new List<string>();
        foreach (string[] wordSet in selectedCluster.Words)
            _words.Add(wordSet[ruleSeed.Next(0, wordSet.Length)]);

        _letterComparisons = ruleSeed.ShuffleFisherYates(Enumerable.Range(0, 4).ToList());
    }

    private void SetPuzzle()
    {
        _chosenWordArrangement = _letterBank.ToList().Shuffle().Take(4).Join("");
        string word = _words.FirstOrDefault(x => x.All(y => _chosenWordArrangement.Contains(y)));

        int corner = (_chosenWordArrangement.IndexOf(word[_letterComparisons[0]]) < _chosenWordArrangement.IndexOf(word[_letterComparisons[1]]) ? 1 : 0) | (_chosenWordArrangement.IndexOf(word[_letterComparisons[2]]) < _chosenWordArrangement.IndexOf(word[_letterComparisons[3]]) ? 2 : 0);

        _position = UnityEngine.Random.Range(0, 16);

        List<int> indices = Enumerable.Range(0, _letterBank.Length).Where(x => _chosenWordArrangement.Contains(_letterBank[x])).ToList();
        int tablePosition;
        switch (indices.Where(x => x < 3).Count())
        {
            case 1:
                //top row going right
                tablePosition = indices.FirstOrDefault(x => x < 3);
                break;
            case 2:
                //intersection of missing row and column component
                tablePosition = Enumerable.Range(0, 3).FirstOrDefault(x => !indices.Contains(x)) + 4 * Enumerable.Range(3, 3).FirstOrDefault(x => !indices.Contains(x)) - 8;
                break;
            case 3:
                //right column going down
                tablePosition = 4 * indices.FirstOrDefault(x => x >= 3) - 5;
                break;
            default:
                throw new Exception("Something went very wrong with letter counting to determine the table position");
        }

        int shiftX = (tablePosition + 1) % 4;
        int shiftY = tablePosition / 4;

        int goalPosition = (((_position % 2 == 1 ? shiftY : 4 - shiftY) + _position / 4) % 4) * 4 + (((_position / 4) % 2 == 0 ? shiftX : 4 - shiftX) + _position) % 4;
        int targetNode = goalPosition % 4 + (corner & 1) + 5 * (goalPosition / 4 + ((corner & 2) >> 1));
        _goalChar = GridLocate(targetNode);

        Log("The position of the statuslight is {0}.", "ABCD"[_position % 4].ToString() + "1234"[_position / 4].ToString());
        Log("The letters selected, in order, are: {0}.", _chosenWordArrangement.Join(", "));
        Log("The associated word is {0}, found at position {1}.", word, "ABCD"[tablePosition % 4].ToString() + "1234"[tablePosition / 4].ToString());
        Log("Comparing letter positions gives the relative corner to use to be {0}.", new string[] { "top left", "top right", "bottom left", "bottom right" }[corner]);
        Log("The target position is {0}.", "ABCD"[goalPosition % 4].ToString() + "1234"[goalPosition / 4].ToString());
        Log("The target letter is {0}, which should be inputted as '{1}'.", _goalChar, Morsify(_goalChar).Replace("..", "-").Replace(" ", ""));
    }

    private void SetVisuals()
    {
        ModuleBg.localPosition = new Vector3(_position % 4 < 2 ? -1 : 1, 0, _position / 4 < 2 ? 1 : -1) * 0.05f;
        ModuleBg.localScale = new Vector3(_position % 2 == 0 ? -1 : 1, 1, _position / 4 % 2 == 0 ? 1 : -1) * 0.1f;
        StatusLightParent.localPosition -= new Vector3(_position % 4 < 2 ? 1 : 0, 0, _position / 4 < 2 ? 0 : 1) * 0.1f + new Vector3(_position % 2 == 0 ? 0.025167f : 0, 0, _position / 4 % 2 == 0 ? 0 : 0.026057f) * 2;
        InputCasing.localPosition = new Vector3(_position % 4 < 2 ? -1 : 1, 0, _position / 4 < 2 ? 1 : -1) * 0.05f + new Vector3(_position % 2 == 0 ? 1 : -1, 0.5f, 1) * 0.02f;
        OutputCasing.localPosition = new Vector3(_position % 4 < 2 ? -1 : 1, 0.025f, _position / 4 < 2 ? 1 : -1) * 0.05f + new Vector3(_position % 2 == 0 ? -1 : 1, 0.75f, _position / 4 % 2 == 0 ? -1 : 1) * 0.02f;
    }

    private void Signal()
    {
        _inputs.Add(_lastInput);
        _lastInput = 0;
        List<char> validity = VerifyInputs();
        if (validity == null)
        {
            HandleInvalidSignal();
        }
    }

    private void HandleInvalidSignal()
    {
        _inputs.Clear();
        _lastInput = -3;
        _held = false;
        Log("The signal transmitted was not precise enough. Strike!");
        GetComponent<KMBombModule>().HandleStrike();
    }

    //verifies inputs and returns the valid characters, or null if the input is invalid
    private List<char> VerifyInputs()
    {
        if (_inputs.Count <= 1)
            return new List<char>();

        if (!ValidateInputs())
            return null;

        List<float> allowedLengths = new List<float>();
        for (int i = 1; i < _inputs.Count; i++)
        {
            allowedLengths.Add(_inputs[i]);
            if (i % 2 == 1)
                allowedLengths.Add(_inputs[i] / 2f);
            if (i % 2 == 1)
                allowedLengths.Add(_inputs[i] * 2f);
        }
        allowedLengths = allowedLengths.OrderBy(x => x).ToList();
        List<float> trialThresholds = new List<float>();
        for (int i = 0; i < allowedLengths.Count - 1; i++)
            trialThresholds.Add((allowedLengths[i] + allowedLengths[i + 1]) / 2f);

        List<string> results = new List<string>();
        List<float> relevantLengths = _inputs.Skip(1).ToList();
        if (_inputs.Count % 2 == 1)
            relevantLengths.Add(_lastInput);

        foreach (float threshold in trialThresholds)
        {
            string result = "";
            for (int i = 0; i < relevantLengths.Count; i++)
                result += relevantLengths[i] < threshold / 2 || relevantLengths[i] > threshold * 2 ? 'X' : (i % 2 == 0 ? (relevantLengths[i] < threshold ? '.' : '-') : (relevantLengths[i] < threshold ? ' ' : 'X'));
            if (result.Contains('X'))
                continue;
            results.Add(result.Replace("-", ".."));
        }
        List<char> resultLetters = Codes.Where(x => results.Contains(x.Value)).Select(x => _alphabetMap[ALPHABET.IndexOf(x.Key)]).OrderBy(x => x).ToList();

        return resultLetters;
    }

    private bool ValidateInputs()
    {
        if (_inputs.Count <= 2)
            return true;
        IEnumerable<float> gaps = Enumerable.Range(0, _inputs.Count).Skip(1).Where(x => x % 2 == 0).Select(x => _inputs[x]);

        float lowerBound = Mathf.Max(gaps.Max() / 2f, _inputs.Skip(1).Max() / 4f);
        float upperBound = _inputs.Min() * 4f;

        if (_inputs.Skip(1).Any(x => x < lowerBound || x > upperBound))
            return false;
        if (gaps.Max() > upperBound / 2)
            return false;
        if (_held && _lastInput > upperBound)
            return false;
        return true;
    }

    private string Morsify(char c)
    {
        return Codes[ALPHABET[_alphabetMap.IndexOf(c)]];
    }

    private char GridLocate(int position)
    {
        return _alphabetMap[ALPHABET.IndexOf(LetterGrid[position])];
    }

    private void Log(string text, params object[] args)
    {
        Debug.LogFormat("[Morsel Code #{0}] {1}", _moduleId, string.Format(text, args));
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "'!{0} transmit .-..' to transmit the code .-..";
#pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        yield return null;

        command = command.ToLowerInvariant();
        string[] commands = command.Split(' ');
        if (commands.Length == 2 && commands[0] == "transmit" && commands[1].Length >= 1 && commands[1].Length <= 5)
        {
            if (commands[1].Any(x => !".-".Contains(x)))
            {
                yield return "sendtochaterror Invalid command.";
                yield break;
            }

            yield return new WaitUntil(() => _lastInput >= 0);
            foreach (char c in commands[1])
            {
                Input.OnInteract();
                _inputs[_inputs.Count - 1] = 0.25f;
                float holdTime = c == '-' ? 0.5f : 0.25f;
                yield return new WaitForSeconds(holdTime);
                Input.OnInteractEnded();
                _inputs[_inputs.Count - 1] = holdTime;
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitUntil(() => _inputs.Count == 0);
        }
        else
        {
            yield return "sendtochaterror Invalid command.";
            yield break;
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (true)
        {
            while (_lastInput < 0 || _inputs.Count > 0)
                yield return true;
            if (_isSolved)
                yield break;
            foreach (char c in Morsify(_goalChar).Replace("..", "-").Replace(" ", ""))
            {
                Input.OnInteract();
                _inputs[_inputs.Count - 1] = 0.1f;
                float holdTime = c == '-' ? 0.2f : 0.1f;
                yield return new WaitForSeconds(holdTime);
                Input.OnInteractEnded();
                _inputs[_inputs.Count - 1] = holdTime;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    //awfully slow program to get all word combinations available for a module like this one
    private IEnumerator LogAll()
    {
        List<WordCluster> clusters = null;
        new Thread(() =>
        {
            try
            {
                clusters = WordCluster.GenerateAllCompleteClusters(6, 4, FourLetterWords.WordList);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }).Start();
        yield return new WaitUntil(() => clusters != null);
        foreach (WordCluster item in clusters.Where(x => x.Letterbank.Length > 0))
        {
            yield return null;
            Debug.Log(item);
        }
        Debug.Log("done");
    }
}

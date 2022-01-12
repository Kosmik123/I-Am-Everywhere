using UnityEngine;
using UnityEditor;  // przestrze� nazw konieczna do tworzenia w�asnego widoku w inspektorze

/*
    Teoretycznie klas� edytora (GeneratorEditor) powinno si� definiowa� w oddzielnym pliku,
    niezale�nie od klasy kt�r� edytuje (Generator), ale ja lubi� zdefiniowa� j� wewn�trz
    
    Po pierwsze, dlatego �e �atwiej mi potem znale�� i modyfikowa� edytora
    Po drugie, dlatego �e w ten spos�b w edytorze mam te� dost�p do prywatnych p�l i funkcji klasy

*/


public class Generator : MonoBehaviour
{
    // metoda kt�r� b�dziemy wywo�ywa� za pomoc� przycisku (raczej public, no chyba �e definiuj� edytor wewn�trz edytowanej klasy)
    public void Generate()
    {
        Debug.Log("THOTH");
    }


    // u�ywam tej dyrektywy preprocesora (#if UNITY_EDITOR), �eby nie kompilowa�o edytora w przypadku buildowania gry, a tylko w edytorze
    // z tego co wiem klasy edytora powoduj� b��dy w czasie buildowania gry, poza tym inspektor i tak w gotowym produkcie nie jest potrzebny
#if UNITY_EDITOR

    // przed zdefiniowaniem klasy edytora trzeba to wstawi�, �eby unity wiedzia�o kt�rej klasy jest to edytor
    [CustomEditor(typeof(Generator))]
    public class GeneratorEditor : Editor // potem definiujesz klas� edytora nazw� najlepiej da� tak� �eby by�o wiadamo czego to jest edytor. Poza tym musi dziedziczy� z klasy Editor 
    {

        // metoda odpowiedzialna za rysowanie inspekora (jak zaczniesz pisa� override to powinno odpowiednio uzupe�ni�� automatycznie
        public override void OnInspectorGUI()
        {
            // potrzebujemy referencji do naszego edytowanego obiektu. Znajduje si� on w zmiennej target
            // ale �eby u�ywa� metod naszej klasy trzeba ten obiekt rzutowa� na nasz� klas� za pomoc� s�owa as
            Generator gen = target as Generator;

            // to te� powinno si� automatycznie pojawi�. Odpowiedzialne jest po prostu za narysowanie w inspektorze wszystkich kontrolek publicznych i serializowanych p�l
            base.OnInspectorGUI();


            // i na koniec pod wszystkim wrzucasz przycisk wewnatrz if'a
            if (GUILayout.Button(" GENERUJ ")) // w argumencie funkcji Button wstawiasz tekst kt�ry ma by� w buttonie
            {
                gen.Generate(); // wywo�anie metody Generate na naszym obiekcie w momencie naci�ni�cia przycisku 
            }
        }
    }
#endif
    // zako�czenie dyrektywy preprocesora

}
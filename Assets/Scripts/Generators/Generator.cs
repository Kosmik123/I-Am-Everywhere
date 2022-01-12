using UnityEngine;
using UnityEditor;  // przestrzeñ nazw konieczna do tworzenia w³asnego widoku w inspektorze

/*
    Teoretycznie klasê edytora (GeneratorEditor) powinno siê definiowaæ w oddzielnym pliku,
    niezale¿nie od klasy któr¹ edytuje (Generator), ale ja lubiê zdefiniowaæ j¹ wewn¹trz
    
    Po pierwsze, dlatego ¿e ³atwiej mi potem znaleŸæ i modyfikowaæ edytora
    Po drugie, dlatego ¿e w ten sposób w edytorze mam te¿ dostêp do prywatnych pól i funkcji klasy

*/


public class Generator : MonoBehaviour
{
    // metoda któr¹ bêdziemy wywo³ywaæ za pomoc¹ przycisku (raczej public, no chyba ¿e definiujê edytor wewn¹trz edytowanej klasy)
    public void Generate()
    {
        Debug.Log("THOTH");
    }


    // u¿ywam tej dyrektywy preprocesora (#if UNITY_EDITOR), ¿eby nie kompilowa³o edytora w przypadku buildowania gry, a tylko w edytorze
    // z tego co wiem klasy edytora powoduj¹ b³êdy w czasie buildowania gry, poza tym inspektor i tak w gotowym produkcie nie jest potrzebny
#if UNITY_EDITOR

    // przed zdefiniowaniem klasy edytora trzeba to wstawiæ, ¿eby unity wiedzia³o której klasy jest to edytor
    [CustomEditor(typeof(Generator))]
    public class GeneratorEditor : Editor // potem definiujesz klasê edytora nazwê najlepiej daæ tak¹ ¿eby by³o wiadamo czego to jest edytor. Poza tym musi dziedziczyæ z klasy Editor 
    {

        // metoda odpowiedzialna za rysowanie inspekora (jak zaczniesz pisaæ override to powinno odpowiednio uzupe³niœæ automatycznie
        public override void OnInspectorGUI()
        {
            // potrzebujemy referencji do naszego edytowanego obiektu. Znajduje siê on w zmiennej target
            // ale ¿eby u¿ywaæ metod naszej klasy trzeba ten obiekt rzutowaæ na nasz¹ klasê za pomoc¹ s³owa as
            Generator gen = target as Generator;

            // to te¿ powinno siê automatycznie pojawiæ. Odpowiedzialne jest po prostu za narysowanie w inspektorze wszystkich kontrolek publicznych i serializowanych pól
            base.OnInspectorGUI();


            // i na koniec pod wszystkim wrzucasz przycisk wewnatrz if'a
            if (GUILayout.Button(" GENERUJ ")) // w argumencie funkcji Button wstawiasz tekst który ma byæ w buttonie
            {
                gen.Generate(); // wywo³anie metody Generate na naszym obiekcie w momencie naciœniêcia przycisku 
            }
        }
    }
#endif
    // zakoñczenie dyrektywy preprocesora

}
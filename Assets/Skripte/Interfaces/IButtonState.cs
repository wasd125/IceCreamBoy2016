using UnityEngine;
using System.Collections;

public interface IButtonState  {

    void DoUpdate();

    void ToPressedState();

    void ToReleasedState();

    void ToIdleState();

}

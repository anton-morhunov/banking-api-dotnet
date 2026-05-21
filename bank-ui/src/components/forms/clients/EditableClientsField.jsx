import PrimaryButton from "../../ui/Button/Button.jsx";
import {
    rowStyleText,
    labelStyle,
    valueStyle,
    editableInputStyle
} from "../../../styles/clientDetailsStyles";
function EditableClientField({
    label,
    field,
    editingField,
    editedClient,
    setEditedClient,
    setEditField,
    saveField
}) {
    
    const isEditing = editingField === field;
    
    return(
        <div style={rowStyleText}>

            <span style={labelStyle}>
                {label}
            </span>

            {
                isEditing ? (
                    <input
                        value={editedClient[field]}
                        onChange={(e) =>
                            setEditedClient({
                                ...editedClient,
                                [field]: e.target.value
                            })
                        }
                        style={editableInputStyle}
                    />
                ) : (
                    <span style={valueStyle}>
                        {editedClient[field]}
                    </span>
                )
            }

            {
                isEditing ? (
                    <PrimaryButton
                        type="button"
                        onClick={() => saveField(field)}
                    >
                        Save
                    </PrimaryButton>
                ) : (
                    <PrimaryButton
                        type="button"
                        onClick={() => setEditField(field)}
                    >
                        Edit
                    </PrimaryButton>
                )
            }

        </div>
    );
}

export default EditableClientField;